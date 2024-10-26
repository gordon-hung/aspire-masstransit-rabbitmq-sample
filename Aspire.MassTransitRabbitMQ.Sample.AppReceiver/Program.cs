using Aspire.MassTransitRabbitMQ.Sample.AppReceiver.MessageHandlers;
using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;
using MassTransit.RabbitMqTransport.Configuration;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	foreach (var xmlFileName in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml"))
	{
		c.IncludeXmlComments(xmlFileName);
	}
});

builder.Services.AddSingleton(TimeProvider.System);

//MassTransit
builder.Services.AddMassTransit((IBusRegistrationConfigurator registrationConfigurator) =>
{
	registrationConfigurator.AddConsumer<MessageDirectFirstMessageHandler>();
	registrationConfigurator.AddConsumer<MessageDirectSecondMessageHandler>();

	registrationConfigurator.AddConsumer<MessageFanoutFirstMessageHandler>();
	registrationConfigurator.AddConsumer<MessageFanoutSecondMessageHandler>();

	registrationConfigurator.AddConsumer<MessageTopicFirstMessageHandler>();
	registrationConfigurator.AddConsumer<MessageTopicSecondMessageHandler>();
	registrationConfigurator.AddConsumer<MessageTopicOtherMessageHandler>();

	registrationConfigurator.AddConsumer<MessageRequestResponseMessageHandler>();

	registrationConfigurator.UsingRabbitMq((IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator) =>
		{
			configurator.Host(new Uri(builder.Configuration.GetValue<string>("RABBITMQ:URL")!), h =>
		{
			h.Username(builder.Configuration.GetValue<string>("RABBITMQ:USERNAME")!);
			h.Password(builder.Configuration.GetValue<string>("RABBITMQ:PASSWORD")!);
		});

			configurator.ReceiveEndpoint(
				queueName: "message.direct.first",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Direct;
					endpointConfigurator.Bind("message.direct", c =>
					{
						c.ExchangeType = ExchangeType.Direct;
					});
					endpointConfigurator.Consumer<MessageDirectFirstMessageHandler>(context);
				});

			configurator.ReceiveEndpoint(
				queueName: "message.direct.second",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Direct;
					endpointConfigurator.Bind("message.direct", c =>
					{
						c.ExchangeType = ExchangeType.Direct;
					});
					endpointConfigurator.Consumer<MessageDirectSecondMessageHandler>(context);
				});

			configurator.ReceiveEndpoint(
				queueName: "message.fanout.first",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Direct;
					endpointConfigurator.Bind("message.fanout", c =>
					{
						c.ExchangeType = ExchangeType.Fanout;
					});
					endpointConfigurator.Consumer<MessageFanoutFirstMessageHandler>(context);
				});

			configurator.ReceiveEndpoint(
				queueName: "message.fanout.second",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Direct;
					endpointConfigurator.Bind("message.fanout", c =>
					{
						c.ExchangeType = ExchangeType.Fanout;
					});
					endpointConfigurator.Consumer<MessageFanoutSecondMessageHandler>(context);
				});

			configurator.ReceiveEndpoint(
				queueName: "message.topic",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Topic;

					endpointConfigurator.BindDeadLetterQueue(
						exchangeName: "message.topic",
						queueName: "message.topic.first",
						configure: (IRabbitMqQueueBindingConfigurator bindingConfigurator) =>
						{
							bindingConfigurator.ExchangeType = ExchangeType.Topic;
							bindingConfigurator.RoutingKey = "message.topic.routing.key.*";
						});
					endpointConfigurator.BindDeadLetterQueue(
						exchangeName: "message.topic",
						queueName: "message.topic.second",
						configure: (IRabbitMqQueueBindingConfigurator bindingConfigurator) =>
						{
							bindingConfigurator.ExchangeType = ExchangeType.Topic;
							bindingConfigurator.RoutingKey = "message.topic.routing.key.*";
						});
					endpointConfigurator.BindDeadLetterQueue(
						exchangeName: "message.topic",
						queueName: "message.topic.other",
						configure: (IRabbitMqQueueBindingConfigurator bindingConfigurator) =>
						{
							bindingConfigurator.ExchangeType = ExchangeType.Topic;
							bindingConfigurator.RoutingKey = "message.topic.routing.other.*";
						});
				});

			configurator.ReceiveEndpoint(
				queueName: "message.topic.first",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Direct;
					endpointConfigurator.Consumer<MessageTopicFirstMessageHandler>(context);
				});

			configurator.ReceiveEndpoint(
				queueName: "message.topic.second",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Direct;
					endpointConfigurator.Consumer<MessageTopicSecondMessageHandler>(context);
				});

			configurator.ReceiveEndpoint(
				queueName: "message.topic.other",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.ExchangeType = ExchangeType.Direct;
					endpointConfigurator.Consumer<MessageTopicOtherMessageHandler>(context);
				});

			configurator.ReceiveEndpoint(
				queueName: "message.request.response",
				configureEndpoint: (IRabbitMqReceiveEndpointConfigurator endpointConfigurator) =>
				{
					endpointConfigurator.Consumer<MessageRequestResponseMessageHandler>(context);
				});
		});
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
