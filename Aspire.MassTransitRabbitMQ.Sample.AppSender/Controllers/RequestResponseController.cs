using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.MassTransitRabbitMQ.Sample.AppSender.Controllers;

[Route("[controller]")]
[ApiController]
public class RequestResponseController(
	TimeProvider timeProvider,
	IRequestClient<MessageRequest> client) : ControllerBase
{
	/// <summary>
	/// Fanouts the first asynchronous.
	/// </summary>
	/// <param name="content">The content.</param>
	/// <returns></returns>
	[HttpPost()]
	public async ValueTask<Response<MessageResponse>> FanoutFirstAsync(
		[FromBody] string content = "Hello")
	{
		var request = client.Create(
			message: new MessageRequest
			{
				SendAt = timeProvider.GetLocalNow(),
				Content = content
			},
			cancellationToken: HttpContext.RequestAborted);
		return (await request.GetResponse<MessageResponse>().ConfigureAwait(false));
	}
}
