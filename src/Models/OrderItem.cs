namespace CSharpSyntheticRepo.Models;

public sealed record OrderItem(
    string Sku,
    int Quantity,
    decimal UnitPrice,
    string Currency
);


