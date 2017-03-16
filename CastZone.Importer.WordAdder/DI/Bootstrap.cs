using CastZone.Importer.WordAdder.Persistences;
using CastZone.Importer.WordAdder.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Importer.WordAdder.DI
{
    public static class Config
    {
        private static Container _container;
        public static Container Container()
        {
            return (_container ?? (_container = new Container(_ =>
            {
                _.For<IWordAdderService>().Use<WordAdderService>();
                _.For<IWordService>().Use<WordService>();
                _.For<IWordPersistence>().Use<SqlWordPersistence>();
                _.For<IQueueService>().Use<QueueService>();
            })));
        }
    }
}
