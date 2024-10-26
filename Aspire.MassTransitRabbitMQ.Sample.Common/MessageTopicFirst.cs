namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageTopicFirst
{
	public DateTimeOffset SendAt { get; set; }

	public string Content { get; set; } = default!;
}
