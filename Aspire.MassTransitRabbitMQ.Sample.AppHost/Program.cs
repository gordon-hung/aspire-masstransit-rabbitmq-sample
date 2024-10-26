using YamlDotNet.Core.Tokens;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitMQUrl = builder.AddParameter("Url", secret: true);
var rabbitMQUsername = builder.AddParameter("Username", secret: true);
var rabbitMQPassword = builder.AddParameter("Password", secret: true);

builder.AddProject<Projects.Aspire_MassTransitRabbitMQ_Sample_AppSender>("aspire-app-sender")
	.WithEnvironment("RABBITMQ:URL", rabbitMQUrl)
	.WithEnvironment("RABBITMQ:USERNAME", rabbitMQUsername)
	.WithEnvironment("RABBITMQ:PASSWORD", rabbitMQPassword);

builder.AddProject<Projects.Aspire_MassTransitRabbitMQ_Sample_AppReceiver>("aspire-app-receiver")
	.WithEnvironment("RABBITMQ:URL", rabbitMQUrl)
	.WithEnvironment("RABBITMQ:USERNAME", rabbitMQUsername)
	.WithEnvironment("RABBITMQ:PASSWORD", rabbitMQPassword);

builder.Build().Run();
