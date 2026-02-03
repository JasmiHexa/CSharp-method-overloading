using CSharpSyntheticRepo.Common;
using CSharpSyntheticRepo.Infrastructure;

namespace CSharpSyntheticRepo.Services;

public enum NotificationTemplate
{
    Receipt,
    ShippingUpdate
}

public sealed class NotificationService
{
    private readonly ConsoleLogger _logger;

    public NotificationService(ConsoleLogger logger) => _logger = logger;

    // Overload set #9
    public Result Send(string email, string subject, string body)
    {
        _logger.Log(LogLevel.Info, $"Email to {email}: {subject} (len={body.Length})");
        return Result.Ok();
    }

    // Overload set #9
    public Result Send(string email, NotificationTemplate template, object model)
    {
        var subject = template switch
        {
            NotificationTemplate.Receipt => "Your receipt",
            NotificationTemplate.ShippingUpdate => "Your shipment",
            _ => "Notification"
        };

        var body = JsonUtil.Serialize(model);
        return Send(email, subject, body);
    }
}


