using CastZone.Importer.WordsScheduler.Persistences;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CastZone.Importer.WordsScheduler.Services
{
    public interface IWordService
    {
        Task<IEnumerable<Word>> GetWordsAsync();
    }
}
