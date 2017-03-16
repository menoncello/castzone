using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastZone.Tools.Pipes;
using RabbitMQ.Client;
using CastZone.Importer.WordsScheduler.Persistences;

namespace CastZone.Importer.WordsScheduler.Services
{
    public class AddQueueService : IAddQueueService
    {
        public void Enqueue(IEnumerable<Word> words) =>
            words.EnqueueMessage("add-word");
    }
}
