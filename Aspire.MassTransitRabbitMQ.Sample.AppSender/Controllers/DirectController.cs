using System.Text.Json;
using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

using RabbitMQ.Client;

namespace Aspire.MassTransitRabbitMQ.Sample.AppSender.Controllers;

[Route("[controller]")]
[ApiController]
public class DirectController(
	TimeProvider timeProvider,
	IBus bus) : ControllerBase
{
	/// <summary>
	/// Directs the first asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost("first")]
	public async ValueTask DirectFirstAsync(
		[FromBody] string content = "direct-first")
		=> await bus.Publish(
			message: new MessageDirectFirst
			{
				SendAt = timeProvider.GetLocalNow(),
				Content = content
			},
			cancellationToken: HttpContext.RequestAborted)
		.ConfigureAwait(false);

	/// <summary>
	/// Directs the second asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost("second")]
	public async ValueTask DirectSecondAsync(
		[FromBody] string content = "direct-second")
		=> await bus.Publish(
			message: new MessageDirectSecond
			{
				SendAt = timeProvider.GetLocalNow(),
				Content = content
			},
			cancellationToken: HttpContext.RequestAborted)
		.ConfigureAwait(false);
}
