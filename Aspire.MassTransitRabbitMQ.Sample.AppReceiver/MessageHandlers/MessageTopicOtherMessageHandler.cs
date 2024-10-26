using System.Text.Json;

using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;

namespace Aspire.MassTransitRabbitMQ.Sample.AppReceiver.MessageHandlers;

public class MessageTopicOtherMessageHandler(
	ILogger<MessageTopicOtherMessageHandler> logger,
	TimeProvider timeProvider) : IConsumer<MessageTopic>
{
	public Task Consume(ConsumeContext<MessageTopic> context)
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
