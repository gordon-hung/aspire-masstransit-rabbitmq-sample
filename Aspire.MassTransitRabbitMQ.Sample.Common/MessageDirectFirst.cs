namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageDirectFirst
{
	public DateTimeOffset SendAt { get; set; }

	public string Content { get; set; } = default!;
}
