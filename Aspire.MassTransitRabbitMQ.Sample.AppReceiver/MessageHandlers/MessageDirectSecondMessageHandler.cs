using System.Text.Json;

using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;

namespace Aspire.MassTransitRabbitMQ.Sample.AppReceiver.MessageHandlers;

public class MessageDirectSecondMessageHandler(
	ILogger<MessageDirectSecondMessageHandler> logger,
	TimeProvider timeProvider) : IConsumer<MessageDirectSecond>
{
	public Task Consume(ConsumeContext<MessageDirectSecond> context)
	{
		logger.LogInformation("{logInformation}",
			new
			{
				LogOn = timeProvider.GetLocalNow().ToString("u"),
				Data = JsonSerializer.Serialize(context.Message)
			});

		return Task.CompletedTask;
	}
}
