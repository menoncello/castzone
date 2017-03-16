using System.Collections.Generic;
using System.Threading.Tasks;

namespace CastZone.Importer.WordAdder.Persistences
{
    public interface IWordPersistence
    {
        Task<bool> ExistsAsync(string word);
        Task InsertAsync(Word word);
    }
}
