namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageTopic
{
	public DateTimeOffset SendAt { get; set; }

	public string Content { get; set; } = default!;
}
