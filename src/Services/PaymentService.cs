using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Infrastructure;

namespace CSharpSyntheticRepo.Services;

public sealed record PaymentMethod(string Kind, string Last4);

public sealed class PaymentService
{
    private readonly ConsoleLogger _logger;

    public PaymentService(ConsoleLogger logger) => _logger = logger;

    // Overload set #8
    public Result<string> Charge(decimal amount, string currency)
        => Charge(amount, currency, new PaymentMethod("CARD", "4242"));

    // Overload set #8 (same param count as (decimal, string) but different param types)
    public Result<string> Charge(int amountCents, int currencyNumericCode)
    {
        var currency = currencyNumericCode switch
        {
            840 => "USD",
            978 => "EUR",
            _ => "UNK"
        };
        return Charge(amountCents / 100m, currency);
    }

    // Overload set #8
    public Result<string> Charge(decimal amount, string currency, PaymentMethod method)
    {
        if (amount <= 0) return Result<string>.Fail("Amount must be > 0");
        var auth = $"AUTH-{method.Kind}-{method.Last4}-{DateTime.UtcNow:HHmmss}";
        _logger.Log(LogLevel.Info, $"Charged {amount:0.00} {currency} via {method.Kind} ****{method.Last4} => {auth}");
        return Result<string>.Ok(auth);
    }
}