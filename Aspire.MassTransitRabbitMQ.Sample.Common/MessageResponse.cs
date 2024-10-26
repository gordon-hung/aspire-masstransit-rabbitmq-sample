namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageResponse
{
	public DateTimeOffset SendAt { get; set; }

	public DateTimeOffset ResponseAt { get; set; }

	public string Content { get; set; } = default!;
}
