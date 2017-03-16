using CastZone.Importer.WordAdder.Persistences;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CastZone.Importer.WordAdder.Services
{
    public interface IWordService
    {
        Task<bool> ExistsAsync(string word);
        Task InsertAsync(Word word);
    }
}
