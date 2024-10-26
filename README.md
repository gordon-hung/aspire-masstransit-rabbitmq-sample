# aspire-masstransit-rabbitmq-sample
ASP.NET Core 8.0 Aspire MassTransit RabbitMQ Sample

RabbitMQ Transport
RabbitMQ is an open-source message broker software that implements the Advanced Message Queuing Protocol (AMQP). It is written in the Erlang programming language and is built on the Open Telecom Platform framework for clustering and failover.

RabbitMQ can be used to decouple and distribute systems by sending messages between them. It supports a variety of messaging patterns, including point-to-point, publish/subscribe, and request/response.

RabbitMQ provides features such as routing, reliable delivery, and message persistence. It also has a built-in management interface that allows for monitoring and management of the broker, queues, and connections. Additionally, it supports various plugins, such as the RabbitMQ Management Plugin, that provide additional functionality.

## Run RabbitMQ
For this quick start, we recommend running the preconfigured Docker image maintained by the MassTransit team. The container image includes the delayed exchange plug-in and the management interface is enabled.
```sh
docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```

## Configure RabbitMQ
Add the MassTransit.RabbitMQ package to the project.
```sh
dotnet add package MassTransit.RabbitMQ
```