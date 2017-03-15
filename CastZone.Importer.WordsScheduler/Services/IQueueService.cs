using CastZone.Importer.WordsScheduler.Models;
using System.Collections.Generic;

namespace CastZone.Importer.WordsScheduler.Services
{
    public interface IQueueService
    {
        void Enqueue(IEnumerable<Word> words);
    }
}
