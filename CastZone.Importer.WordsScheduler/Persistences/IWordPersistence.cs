using System.Collections.Generic;
using System.Threading.Tasks;

namespace CastZone.Importer.WordsScheduler.Persistences
{
    public interface IWordPersistence
    {
        Task<IEnumerable<Word>> GetWordsAsync();
    }
}
