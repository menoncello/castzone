using CastZone.Importer.WordsScheduler.Persistences;
using CastZone.Importer.WordsScheduler.Services;
using CastZone.Tools.Aspect;
using CastZone.Tools.Logging;
using CastZone.Tools.Pipes;
using StructureMap;

namespace CastZone.Importer.WordsScheduler.DI
{
    public static class Bootstrap
    {
        public static void Configure() =>
            Factory.Container.Configure(_ =>
            {
                _.DefaultSetup();

                _.For<IWordSchedulerService>().Use<WordSchedulerService>().Proxy();

                _.For<IWordService>().Use<WordService>().Proxy();
                _.For<IWordPersistence>().Use<SqlWordPersistence>().Proxy();
                _.For<IQueueService>().Use<QueueService>().Proxy();
                _.For<IAddQueueService>().Use<AddQueueService>().Proxy();
            });
    }
}
