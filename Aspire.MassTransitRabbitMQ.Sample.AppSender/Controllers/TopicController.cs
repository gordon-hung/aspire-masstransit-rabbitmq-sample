using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.MassTransitRabbitMQ.Sample.AppSender.Controllers;

[Route("[controller]")]
[ApiController]
public class TopicController(
	TimeProvider timeProvider,
	IBus bus) : ControllerBase
{
	/// <summary>
	/// Topics the first asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost("routing-key-first")]
	public async ValueTask TopicFirstAsync(
		[FromBody] string content = "topic-first")
		=> await bus.Publish(
			message: new MessageTopic
			{
				SendAt = timeProvider.GetLocalNow(),
				Content = content
			},
			callback: ctx =>
			{
				ctx.SetRoutingKey("message.topic.routing.key.first");
			},
			cancellationToken: HttpContext.RequestAborted)
		.ConfigureAwait(false);

	/// <summary>
	/// Topics the second asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost("routing-key-second")]
	public async ValueTask TopicSecondAsync(
		[FromBody] string content = "topic-second")
		=> await bus.Publish(
			message: new MessageTopic
			{
				SendAt = timeProvider.GetLocalNow(),
				Content = content
			},
			callback: ctx =>
			{
				ctx.SetRoutingKey("message.topic.routing.key.second");
			},
			cancellationToken: HttpContext.RequestAborted)
		.ConfigureAwait(false);

	/// <summary>
	/// Topics the other asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost("routing-other-other")]
	public async ValueTask TopicOtherAsync(
		[FromBody] string content = "topic-other")
		=> await bus.Publish(
			message: new MessageTopic
			{
				SendAt = timeProvider.GetLocalNow(),
				Content = content
			},
			callback: ctx =>
			{
				ctx.SetRoutingKey("message.topic.routing.other.other");
			},
			cancellationToken: HttpContext.RequestAborted)
		.ConfigureAwait(false);
}
