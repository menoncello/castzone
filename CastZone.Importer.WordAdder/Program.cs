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

                    ch.QueueDeclare("add-word", true, false, false, null);

                    ch.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(ch);

                    consumer.Received += (model, ea) =>
                    {
                        service
                            .Execute(ea.Body.FromByte<Word>());
                        ch.BasicAck(ea.DeliveryTag, false);
                    };

                    ch.BasicConsume("add-word", false, consumer);

                    Console.ReadLine();
                });
        }
    }
}
