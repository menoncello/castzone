using CastZone.Importer.WordsScheduler.DI;
using CastZone.Importer.WordsScheduler.Services;
using CastZone.Tools.Logging;
using CastZone.Tools.Pipes;
using System;

namespace CastZone.Importer.WordsScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Configure();
            var logger = Factory.Container.GetInstance<ILogger>();

            logger.Info("Starting WordsScheduler service");

            Factory.Container
                .GetInstance<IWordSchedulerService>()
                .ExecuteAsync().Wait();

            logger.Info("Finishing WordsScheduler service");
            Console.ReadLine();
        }
    }
}
