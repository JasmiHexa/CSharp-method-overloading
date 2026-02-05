using CSharpSyntheticRepo.Models;
using CSharpSyntheticRepo.Infrastructure;

using System.Collections.Generic;
using System.Linq;

namespace CSharpSyntheticRepo.Services
{
    public sealed class PricingService
    {
        private readonly ConsoleLogger _logger;

        public PricingService(ConsoleLogger logger)
        {
            _logger = logger;
        }

        // Overload set #5
        public decimal CalculateSubtotal(Order order)
        {
            return CalculateSubtotal(order.Items);
        }

        // Overload set #5
        public decimal CalculateSubtotal(IEnumerable<OrderItem> items)
        {
            return items.Sum(i => CalculateSubtotal(i.UnitPrice, i.Quantity));
        }

        // Overload set #5
        public decimal CalculateSubtotal(decimal unitPrice, int quantity)
        {
            return unitPrice * quantity;
        }

        // Overload set #5 (same param count as (decimal, int) but different param types)
        public decimal CalculateSubtotal(int unitPriceCents, int quantity)
        {
            return (unitPriceCents / 100m) * quantity;
        }

        // Overload set #6
        public decimal CalculateTotal(Order order)
        {
            return CalculateTotal(CalculateSubtotal(order), 0m);
        }

        // Overload set #6
        public decimal CalculateTotal(decimal subtotal, decimal discountAmount)
        {
            return Math.Max(0m, subtotal - discountAmount);
        }

        // Overload set #6 (same param count but different param types)
        public decimal CalculateTotal(decimal subtotal, int discountPercent)
        {
            return CalculateTotal(subtotal, subtotal * (discountPercent / 100m));
        }

        // Overload set #6 (different param count)
        public decimal CalculateTotal(decimal subtotal, decimal discountAmount, decimal shippingFee)
        {
            return Math.Max(0m, subtotal - discountAmount) + Math.Max(0m, shippingFee);
        }

        public void TracePriceBreakdown(Order order)
        {
            var subtotal = CalculateSubtotal(order);
            _logger.Log(LogLevel.Debug, "Subtotal: " + subtotal.ToString("0.00") + " " + order.Currency);
        }
    }
}


