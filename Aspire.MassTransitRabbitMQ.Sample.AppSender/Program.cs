using Aspire.MassTransitRabbitMQ.Sample.Common;
using MassTransit;
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

builder.Services.AddMassTransit(x =>
{
	x.AddRequestClient<MessageRequest>();
	x.UsingRabbitMq((context, config) =>
	{
		config.Host(new Uri(builder.Configuration.GetValue<string>("RABBITMQ:URL")!), h =>
		{
			h.Username(builder.Configuration.GetValue<string>("RABBITMQ:USERNAME")!);
			h.Password(builder.Configuration.GetValue<string>("RABBITMQ:PASSWORD")!);
		});

		config.Message<MessageDirectFirst>(c => { c.SetEntityName("message.direct.first"); });
		config.Publish<MessageDirectFirst>(c => { c.ExchangeType = ExchangeType.Direct; });
		config.Message<MessageDirectSecond>(c => { c.SetEntityName("message.direct.second"); });
		config.Publish<MessageDirectSecond>(c => { c.ExchangeType = ExchangeType.Direct; });

		config.Message<MessageFanout>(c => { c.SetEntityName("message.fanout"); });
		config.Publish<MessageFanout>(c => { c.ExchangeType = ExchangeType.Fanout; });

		config.Message<MessageTopic>(c => { c.SetEntityName("message.topic"); });
		config.Publish<MessageTopic>(c => { c.ExchangeType = ExchangeType.Topic; });
	});
});

builder.Services.AddSingleton(TimeProvider.System);

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
