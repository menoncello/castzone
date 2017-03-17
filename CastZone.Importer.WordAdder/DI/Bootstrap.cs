using CastZone.Importer.WordAdder.Persistences;
using CastZone.Importer.WordAdder.Services;
using CastZone.Tools.Aspect;
using CastZone.Tools.Logging;
using CastZone.Tools.Pipes;
using StructureMap;

namespace CastZone.Importer.WordAdder.DI
{
    public static class Bootstrap
    {
        public static void Configure() =>
            Factory.Container.Configure(_ =>
            {
                _.DefaultSetup();

                _.For<IWordAdderService>().Use<WordAdderService>().Proxy();

                _.For<IWordService>().Use<WordService>().Proxy();
                _.For<IWordPersistence>().Use<SqlWordPersistence>().Proxy();
                _.For<IQueueService>().Use<QueueService>().Proxy().Proxy();
            });
    }
}
