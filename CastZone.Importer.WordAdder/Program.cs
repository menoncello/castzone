using CastZone.Importer.WordAdder.DI;
using CastZone.Importer.WordAdder.Persistences;
using CastZone.Importer.WordAdder.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace CastZone.Importer.WordAdder
{
    class Program
    {
        static void Main(string[] args)
        {
            new ConnectionFactory()
                .OpenChannel(ch =>
                {
                    var service = Config.Container()
                        .GetInstance<IWordAdderService>();

                    ch.QueueDeclare(queue: "add-word",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    ch.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(ch);

                    consumer.Received += (model, ea) =>
                    {
                        service
                            .Execute(ea.Body.FromByte<Word>());
                        ch.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    ch.BasicConsume(queue: "add-word",
                                 noAck: false,
                                 consumer: consumer);

                    Console.ReadLine();
                });
        }
    }
}
