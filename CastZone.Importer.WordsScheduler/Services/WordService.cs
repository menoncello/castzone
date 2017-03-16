using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastZone.Importer.WordsScheduler.Persistences;

namespace CastZone.Importer.WordsScheduler.Services
{
    public class WordService : IWordService
    {
        private readonly IWordPersistence _wordPersistence;

        public WordService(IWordPersistence wordPersistence)
        {
            _wordPersistence = wordPersistence;
        }

        public async Task<IEnumerable<Word>> GetWordsAsync()
        {
            return await _wordPersistence.GetWordsAsync();
        }
    }
}
