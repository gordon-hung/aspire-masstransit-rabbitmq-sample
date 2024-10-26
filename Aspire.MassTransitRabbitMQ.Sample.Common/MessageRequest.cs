﻿namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageRequest
{
	public DateTimeOffset SendAt { get; set; }

	public string Content { get; set; } = default!;
}