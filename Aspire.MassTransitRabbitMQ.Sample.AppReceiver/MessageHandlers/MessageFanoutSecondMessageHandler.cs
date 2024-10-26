using System.Text.Json;

using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;

namespace Aspire.MassTransitRabbitMQ.Sample.AppReceiver.MessageHandlers;

public class MessageFanoutSecondMessageHandler(
	ILogger<MessageFanoutSecondMessageHandler> logger,
	TimeProvider timeProvider) : IConsumer<MessageFanout>
{
	public Task Consume(ConsumeContext<MessageFanout> context)
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
