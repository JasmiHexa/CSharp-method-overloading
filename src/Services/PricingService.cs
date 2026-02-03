using CSharpSyntheticRepo.Models;
using CSharpSyntheticRepo.Infrastructure;

namespace CSharpSyntheticRepo.Services;

public sealed class PricingService
{
    private readonly ConsoleLogger _logger;

    public PricingService(ConsoleLogger logger) => _logger = logger;

    // Overload set #5
    public decimal CalculateSubtotal(Order order)
        => CalculateSubtotal(order.Items);

    // Overload set #5
    public decimal CalculateSubtotal(IEnumerable<OrderItem> items)
        => items.Sum(i => CalculateSubtotal(i.UnitPrice, i.Quantity));

    // Overload set #5
    public decimal CalculateSubtotal(decimal unitPrice, int quantity)
        => unitPrice * quantity;

    // Overload set #5 (same param count as (decimal, int) but different param types)
    public decimal CalculateSubtotal(int unitPriceCents, int quantity)
        => (unitPriceCents / 100m) * quantity;

    // Overload set #6
    public decimal CalculateTotal(Order order)
        => CalculateTotal(CalculateSubtotal(order), discountAmount: 0m);

    // Overload set #6
    public decimal CalculateTotal(decimal subtotal, decimal discountAmount)
        => Math.Max(0m, subtotal - discountAmount);

    // Overload set #6 (same param count but different param types)
    public decimal CalculateTotal(decimal subtotal, int discountPercent)
        => CalculateTotal(subtotal, subtotal * (discountPercent / 100m));

    // Overload set #6 (different param count)
    public decimal CalculateTotal(decimal subtotal, decimal discountAmount, decimal shippingFee)
        => Math.Max(0m, subtotal - discountAmount) + Math.Max(0m, shippingFee);

    public void TracePriceBreakdown(Order order)
    {
        var subtotal = CalculateSubtotal(order);
        _logger.Log(LogLevel.Debug, $"Subtotal: {subtotal:0.00} {order.Currency}");
    }
}


