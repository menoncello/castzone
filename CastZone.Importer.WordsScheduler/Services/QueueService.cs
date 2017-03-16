using System.Collections.Generic;
using RabbitMQ.Client;
using CastZone.Importer.WordsScheduler.Persistences;

namespace CastZone.Importer.WordsScheduler.Services
{
    public class QueueService : IQueueService
    {
        public void Enqueue(IEnumerable<Word> words) =>
            words.EnqueueMessage("word");
    }
}
