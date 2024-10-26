using System.Text.Json;
using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;

namespace Aspire.MassTransitRabbitMQ.Sample.AppReceiver.MessageHandlers;

public class MessageRequestResponseMessageHandler(
	ILogger<MessageRequestResponseMessageHandler> logger,
	TimeProvider timeProvider) : IConsumer<MessageRequest>
{
	public async Task Consume(ConsumeContext<MessageRequest> context)
	{
		logger.LogInformation("{logInformation}",
			new
			{
				LogOn = timeProvider.GetLocalNow().ToString("u"),
				Data = JsonSerializer.Serialize(context.Message)
			});

		var data = context.Message;

		var response = new MessageResponse()
		{
			SendAt = data.SendAt,
			ResponseAt = timeProvider.GetUtcNow(),
			Content = data.Content
		};

		await context.RespondAsync(response).ConfigureAwait(false);
	}
}
