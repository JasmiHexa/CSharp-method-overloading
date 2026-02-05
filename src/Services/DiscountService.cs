using CSharpSyntheticRepo.Infrastructure;
using CSharpSyntheticRepo.Models;

namespace CSharpSyntheticRepo.Services
{
    public sealed class DiscountService
    {
        private readonly ConsoleLogger _logger;

        public DiscountService(ConsoleLogger logger)
        {
            _logger = logger;
        }

        // Overload set #7
        public decimal ApplyDiscount(Order order)
        {
            // default: 5% if order has 3+ items
            var pct = order.Items.Sum(i => i.Quantity) >= 3 ? 0.05m : 0m;
            return ApplyDiscount(order.Items.Sum(i => i.UnitPrice * i.Quantity), pct, 25m);
        }

        // Overload set #7
        public decimal ApplyDiscount(decimal amount, string couponCode)
        {
            var pct = couponCode.Equals("SAVE10", StringComparison.OrdinalIgnoreCase) ? 0.10m : 0m;
            return ApplyDiscount(amount, pct, 40m);
        }

        // Overload set #7
        public decimal ApplyDiscount(decimal amount, decimal percentage, decimal maxDiscount)
        {
            var raw = amount * percentage;
            var applied = Math.Min(raw, maxDiscount);
            _logger.Log(LogLevel.Debug, "Discount applied: " + applied.ToString("0.00"));
            return applied;
        }
    }
}


