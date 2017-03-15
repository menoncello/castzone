using CastZone.Importer.WordsScheduler.DI;
using CastZone.Importer.WordsScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Importer.WordsScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.Container()
                .GetInstance<IWordSchedulerService>()
                .Execute();
        }
    }
}
