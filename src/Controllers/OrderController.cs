using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Infrastructure;
using CSharpSyntheticRepo.Models;
using CSharpSyntheticRepo.Services;

namespace CSharpSyntheticRepo.Controllers;


public sealed class OrderController
{
    private readonly ConsoleLogger _logger;
    private readonly InMemoryOrderRepository _repo;
    private readonly PricingService _pricing;
    private readonly DiscountService _discounts;
    private readonly PaymentService _payments;
    private readonly ExternalShippingClient _shipping;
    private readonly NotificationService _notify;

    public OrderController(
        ConsoleLogger logger,
        InMemoryOrderRepository repo,
        PricingService pricing,
        DiscountService discounts,
        PaymentService payments,
        ExternalShippingClient shipping,
        NotificationService notify)
    {
        _logger = logger;
        _repo = repo;
        _pricing = pricing;
        _discounts = discounts;
        _payments = payments;
        _shipping = shipping;
        _notify = notify;
    }

    public Result<string> PlaceOrder(Order order, string? couponCode)
    {
        if (order.Items.Count == 0) return Result<string>.Fail("Order must contain items");

        _logger.Log($"Placing order {order.Id} for {order.CustomerEmail}");
        _pricing.TracePriceBreakdown(order);

        var subtotal = _pricing.CalculateSubtotal(order);
        var discount = string.IsNullOrWhiteSpace(couponCode)
            ? _discounts.ApplyDiscount(order)
            : _discounts.ApplyDiscount(subtotal, couponCode);

        var total = _pricing.CalculateTotal(subtotal, discount);
        _logger.Log(LogLevel.Info, $"Total: {total:0.00} {order.Currency}");

        var save = _repo.Save(order);
        if (!save.Success) return Result<string>.Fail(save.Error);

        var auth = _payments.Charge(total, order.Currency);
        if (!auth.Success) return Result<string>.Fail(auth.Error);

        var label = _shipping.CreateLabel(order);
        if (!label.Success) return Result<string>.Fail(label.Error);

        var emailModel = new
        {
            order.Id,
            subtotal,
            discount,
            total,
            currency = order.Currency,
            auth = auth.Value,
            tracking = label.Value
        };

        var notify = _notify.Send(order.CustomerEmail, NotificationTemplate.Receipt, emailModel);
        if (!notify.Success) return Result<string>.Fail(notify.Error);

        return Result<string>.Ok(label.Value!);
    }

    // Overload (same method name with different params)
    public Result<string> PlaceOrder(Order order)
        => PlaceOrder(order, couponCode: null);
}


