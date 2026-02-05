using CSharpSyntheticRepo.Controllers;
using CSharpSyntheticRepo.Infrastructure;
using CSharpSyntheticRepo.Services;
using CSharpSyntheticRepo.Workflow;

namespace CSharpSyntheticRepo
{
    internal static class Program
    {
        private static int Main()
        {
            var logger = new ConsoleLogger(minLevel: LogLevel.Info);
            var repo = new InMemoryOrderRepository(logger);
            var pricing = new PricingService(logger);
            var discounts = new DiscountService(logger);
            var payments = new PaymentService(logger);
            var shipping = new ExternalShippingClient(logger);
            var notify = new NotificationService(logger);

            var controller = new OrderController(
                logger,
                repo,
                pricing,
                discounts,
                payments,
                shipping,
                notify
            );

            var app = new AppWorkflow(logger, controller);

            try
            {
                var result = app.Run();
                logger.Log(result.Success ? LogLevel.Info : LogLevel.Error, "Workflow finished: " + result);
                return result.Success ? 0 : 2;
            }
            catch (Exception ex)
            {
                logger.Log(ex, "Fatal error");
                return 1;
            }
        }
    }
}


