using System.Collections.Generic;
using RabbitMQ.Client;
using CastZone.Importer.WordsScheduler.Persistences;
using CastZone.Tools.Aspect;

namespace CastZone.Importer.WordsScheduler.Services
{
    public class QueueService : IQueueService
    {
        [Logging]
        public void Enqueue(IEnumerable<Word> words) =>
            words.EnqueueMessage("word", durable: true);
    }
}
