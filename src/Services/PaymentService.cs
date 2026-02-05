using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Infrastructure;

using System;

namespace CSharpSyntheticRepo.Services
{
    public sealed class PaymentMethod
    {
        public string Kind { get; private set; }
        public string Last4 { get; private set; }

        public PaymentMethod(string kind, string last4)
        {
            Kind = kind;
            Last4 = last4;
        }
    }

    public sealed class PaymentService
    {
        private readonly ConsoleLogger _logger;

        public PaymentService(ConsoleLogger logger)
        {
            _logger = logger;
        }

        // Overload set #8
        public Result<string> Charge(decimal amount, string currency)
        {
            return Charge(amount, currency, new PaymentMethod("CARD", "4242"));
        }

        // Overload set #8 (same param count as (decimal, string) but different param types)
        public Result<string> Charge(int amountCents, int currencyNumericCode)
        {
            string currency;
            switch (currencyNumericCode)
            {
                case 840:
                    currency = "USD";
                    break;
                case 978:
                    currency = "EUR";
                    break;
                default:
                    currency = "UNK";
                    break;
            }

            return Charge(amountCents / 100m, currency);
        }

        // Overload set #8
        public Result<string> Charge(decimal amount, string currency, PaymentMethod method)
        {
            if (amount <= 0) return Result<string>.Fail("Amount must be > 0");

            var auth = "AUTH-" + method.Kind + "-" + method.Last4 + "-" + DateTime.UtcNow.ToString("HHmmss");
            _logger.Log(
                LogLevel.Info,
                "Charged " + amount.ToString("0.00") + " " + currency + " via " + method.Kind + " ****" + method.Last4 + " => " + auth
            );
            return Result<string>.Ok(auth);
        }
    }
}


