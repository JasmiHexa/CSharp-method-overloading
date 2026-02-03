using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Controllers;
using CSharpSyntheticRepo.Infrastructure;
using CSharpSyntheticRepo.Models;

namespace CSharpSyntheticRepo.Workflow;

public sealed class AppWorkflow
{
    private readonly ConsoleLogger _logger;
    private readonly OrderController _controller;

    public AppWorkflow(ConsoleLogger logger, OrderController controller)
    {
        _logger = logger;
        _controller = controller;
    }

    public Result Run()
    {
        var order = new Order
        {
            Id = $"ORD-{DateTime.UtcNow:yyyyMMdd-HHmmss}",
            CustomerEmail = "customer@example.com",
            Items =
            {
                new OrderItem("SKU-CHAIR", 1, 79.99m, "USD"),
                new OrderItem("SKU-LAMP", 2, 19.50m, "USD")
            }
        };

        _logger.Log(LogLevel.Debug, "Order JSON:");
        _logger.Log(JsonUtil.Serialize(order));

        // Exercise overloads explicitly (same name, same param count, different param types)
        var centsSubtotal = new Services.PricingService(_logger).CalculateSubtotal(unitPriceCents: 7999, quantity: 1);
        _logger.Log(LogLevel.Debug, $"Cents subtotal: {centsSubtotal:0.00} USD");

        // Same method name, different params (PlaceOrder(order) vs PlaceOrder(order, couponCode))
        var tracking = _controller.PlaceOrder(order, couponCode: "SAVE10");
        if (!tracking.Success) return Result.Fail(tracking.Error);

        _logger.Log(LogLevel.Info, $"Order completed. Tracking={tracking.Value}");

        // Second order: use PlaceOrder(order) overload (different param count)
        var order2 = new Order
        {
            Id = $"ORD-{DateTime.UtcNow:yyyyMMdd-HHmmss}-B",
            CustomerEmail = "customer2@example.com",
            Items = { new OrderItem("SKU-DESK", 1, 120.00m, "USD") }
        };
        var tracking2 = _controller.PlaceOrder(order2);
        if (!tracking2.Success) return Result.Fail(tracking2.Error);

        _logger.Log(LogLevel.Info, $"Second order completed. Tracking={tracking2.Value}");
        return Result.Ok();
    }
}


