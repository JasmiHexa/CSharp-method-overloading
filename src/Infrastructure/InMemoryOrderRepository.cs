using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Models;

namespace CSharpSyntheticRepo.Infrastructure;

public sealed class InMemoryOrderRepository
{
    private readonly Dictionary<string, Order> _orders = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConsoleLogger _logger;

    public InMemoryOrderRepository(ConsoleLogger logger) => _logger = logger;

    // Overload set #3
    public Result Save(Order order) => Save(order.Id, order);

    // Overload set #3
    public Result Save(string id, Order order)
    {
        _orders[id] = order;
        _logger.Log(LogLevel.Debug, $"Repo saved order {id} (items={order.Items.Count})");
        return Result.Ok();
    }

    public Result<Order> Get(string id)
        => _orders.TryGetValue(id, out var o)
            ? Result<Order>.Ok(o)
            : Result<Order>.Fail($"Order '{id}' not found");
}


