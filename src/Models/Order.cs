namespace CSharpSyntheticRepo.Models;

public sealed class Order
{
    public required string Id { get; init; }
    public required string CustomerEmail { get; init; }
    public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; init; } = new();

    public string Currency => Items.Count == 0 ? "USD" : Items[0].Currency;
}