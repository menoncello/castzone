using System.Threading.Tasks;
using CastZone.Importer.WordAdder.Persistences;
using StructureMap;

namespace CastZone.Importer.WordAdder.Services
{
    public class WordService : IWordService
    {
        private readonly IWordPersistence _wordPersistence;

        [DefaultConstructor]
        public WordService(IWordPersistence wordPersistence)
        {
            _wordPersistence = wordPersistence;
        }

        public async Task<bool> ExistsAsync(string word)
        {
            return await _wordPersistence.ExistsAsync(word);
        }

        public async Task InsertAsync(Word word)
        {
            await _wordPersistence.InsertAsync(word);
        }
    }
}
