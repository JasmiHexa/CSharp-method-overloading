using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Models;

using System;
using System.Collections.Generic;

namespace CSharpSyntheticRepo.Infrastructure
{
    public sealed class InMemoryOrderRepository
    {
        private readonly Dictionary<string, Order> _orders;
        private readonly ConsoleLogger _logger;

        public InMemoryOrderRepository(ConsoleLogger logger)
        {
            _logger = logger;
            _orders = new Dictionary<string, Order>(StringComparer.OrdinalIgnoreCase);
        }

        // Overload set #3
        public Result Save(Order order)
        {
            return Save(order.Id, order);
        }

        // Overload set #3
        public Result Save(string id, Order order)
        {
            _orders[id] = order;
            _logger.Log(LogLevel.Debug, "Repo saved order " + id + " (items=" + order.Items.Count + ")");
            return Result.Ok();
        }

        public Result<Order> Get(string id)
        {
            Order o;
            if (_orders.TryGetValue(id, out o))
            {
                return Result<Order>.Ok(o);
            }
            return Result<Order>.Fail("Order '" + id + "' not found");
        }
    }
}


