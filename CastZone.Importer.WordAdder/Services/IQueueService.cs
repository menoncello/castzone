using CastZone.Importer.WordAdder.Persistences;
using System.Collections.Generic;

namespace CastZone.Importer.WordAdder.Services
{
    public interface IQueueService
    {
        void Enqueue(Word words);
    }
}
