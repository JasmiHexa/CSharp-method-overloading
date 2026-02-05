using System;
using System.Collections.Generic;

namespace CSharpSyntheticRepo.Models
{
    public sealed class Order
    {
        public string Id { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CreatedUtc { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order()
        {
            CreatedUtc = DateTime.UtcNow;
            Items = new List<OrderItem>();
        }

        public string Currency
        {
            get { return Items.Count == 0 ? "USD" : Items[0].Currency; }
        }
    }
}


