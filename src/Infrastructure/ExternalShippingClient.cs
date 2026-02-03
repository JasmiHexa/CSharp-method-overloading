using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Models;

namespace CSharpSyntheticRepo.Infrastructure;

public sealed class ExternalShippingClient
{
    private readonly ConsoleLogger _logger;

    public ExternalShippingClient(ConsoleLogger logger) => _logger = logger;

    // Overload set #4
    public Result<string> CreateLabel(Order order)
        => CreateLabel(order.Id, order.Items.Sum(i => i.Quantity), order.CustomerEmail);

    // Overload set #4
    public Result<string> CreateLabel(string orderId, int itemCount, string destinationEmail)
    {
        // Simulate external call
        var tracking = $"TRK-{orderId}-{itemCount:D2}";
        _logger.Log(LogLevel.Info, $"Shipping label created for {destinationEmail}: {tracking}");
        return Result<string>.Ok(tracking);
    }
}


