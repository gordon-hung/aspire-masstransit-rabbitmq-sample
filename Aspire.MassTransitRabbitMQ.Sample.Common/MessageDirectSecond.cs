﻿namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageDirectSecond
{
	public DateTimeOffset SendAt { get; set; }

	public string Content { get; set; } = default!;
}
