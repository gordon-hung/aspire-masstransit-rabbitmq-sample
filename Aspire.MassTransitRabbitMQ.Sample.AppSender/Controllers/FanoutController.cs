using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.MassTransitRabbitMQ.Sample.AppSender.Controllers;

[Route("[controller]")]
[ApiController]
public class FanoutController(
	TimeProvider timeProvider,
	IBus bus) : ControllerBase
{
	/// <summary>
	/// Fanouts the first asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost("fanout-first")]
	public async ValueTask FanoutFirstAsync(
		[FromBody] string content = "fanout-first")
		=> await bus.Publish(new MessageFanout
		{
			SendAt = timeProvider.GetLocalNow(),
			Content = content
		}).ConfigureAwait(false);

	/// <summary>
	/// Fanouts the second asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost("fanout-second")]
	public async ValueTask FanoutSecondAsync(
		[FromBody] string content = "fanout-second")
		=> await bus.Publish(new MessageFanout
		{
			SendAt = timeProvider.GetLocalNow(),
			Content = content
		}).ConfigureAwait(false);
}
