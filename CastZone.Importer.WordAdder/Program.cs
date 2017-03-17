using CastZone.Importer.WordAdder.DI;
using CastZone.Importer.WordAdder.Persistences;
using CastZone.Importer.WordAdder.Services;
using CastZone.Tools.Logging;
using CastZone.Tools.Pipes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace CastZone.Importer.WordAdder
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Configure();
            var logger = Factory.Container.GetInstance<ILogger>();

            logger.Info("Starting WordAdder service");
            Console.WriteLine(Factory.Container.WhatDoIHave());
            var service = Factory.Container
                .GetInstance<IWordAdderService>();

            new ConnectionFactory()
                .OpenChannel(ch =>
                {
                    logger.Info("Channel opened");

                    //Catcher.TryCatch(() =>
                    //{
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
                    //}, logger.Error);
                    Console.ReadLine();
                });
        }
    }
}
