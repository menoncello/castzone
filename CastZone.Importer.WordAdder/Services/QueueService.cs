using System.Collections.Generic;
using RabbitMQ.Client;
using CastZone.Importer.WordAdder.Persistences;

namespace CastZone.Importer.WordAdder.Services
{
    public class QueueService : IQueueService
    {
        public void Enqueue(Word word) =>
            word.EnqueueMessage("word");
    }
}
