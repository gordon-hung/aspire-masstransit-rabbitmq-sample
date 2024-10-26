namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageFanout
{
	public DateTimeOffset SendAt { get; set; }

	public string Content { get; set; } = default!;
}
