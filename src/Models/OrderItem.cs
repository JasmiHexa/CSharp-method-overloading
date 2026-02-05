namespace CSharpSyntheticRepo.Models
{
    public sealed class OrderItem
    {
        public string Sku { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string Currency { get; private set; }

        public OrderItem(string sku, int quantity, decimal unitPrice, string currency)
        {
            Sku = sku;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Currency = currency;
        }
    }
}


