﻿namespace Aspire.MassTransitRabbitMQ.Sample.Common;

public class MessageTopicSecond
{
	public DateTimeOffset SendAt { get; set; }

	public string Content { get; set; } = default!;
}