using System.Text.Json;

using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;

namespace Aspire.MassTransitRabbitMQ.Sample.AppReceiver.MessageHandlers;

public class MessageDirectFirstMessageHandler(
	ILogger<MessageDirectFirstMessageHandler> logger,
	TimeProvider timeProvider) : IConsumer<MessageDirectFirst>
{
	public Task Consume(ConsumeContext<MessageDirectFirst> context)
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
