using CastZone.Importer.WordsScheduler.Persistences;
using CastZone.Importer.WordsScheduler.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Importer.WordsScheduler.DI
{
    public static class Config
    {
        private static Container _container;
        public static Container Container()
        {
            return (_container ?? (_container = new Container(_ =>
            {
                _.For<IWordSchedulerService>().Use<WordSchedulerService>();
                _.For<IWordService>().Use<WordService>();
                _.For<IWordPersistence>().Use<SqlWordPersistence>();
                _.For<IQueueService>().Use<QueueService>();
                _.For<IAddQueueService>().Use<AddQueueService>();
            })));
        }
    }
}
