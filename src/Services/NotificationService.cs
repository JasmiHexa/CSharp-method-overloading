using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Infrastructure;

namespace CSharpSyntheticRepo.Services
{
    public enum NotificationTemplate
    {
        Receipt,
        ShippingUpdate
    }

    public sealed class NotificationService
    {
        private readonly ConsoleLogger _logger;

        public NotificationService(ConsoleLogger logger)
        {
            _logger = logger;
        }

        // Overload set #9
        public Result Send(string email, string subject, string body)
        {
            _logger.Log(LogLevel.Info, "Email to " + email + ": " + subject + " (len=" + body.Length + ")");
            return Result.Ok();
        }

        // Overload set #9
        public Result Send(string email, NotificationTemplate template, object model)
        {
            string subject;
            switch (template)
            {
                case NotificationTemplate.Receipt:
                    subject = "Your receipt";
                    break;
                case NotificationTemplate.ShippingUpdate:
                    subject = "Your shipment";
                    break;
                default:
                    subject = "Notification";
                    break;
            }

            var body = JsonUtil.Serialize(model);
            return Send(email, subject, body);
        }
    }
}


