using CastZone.Importer.WordsScheduler.Models;
using System.Collections.Generic;

namespace CastZone.Importer.WordsScheduler.Services
{
    public interface IAddQueueService
    {
        void Enqueue(IEnumerable<Word> words);
    }
}
