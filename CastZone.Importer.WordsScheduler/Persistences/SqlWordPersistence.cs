using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastZone.Tools.Pipes;
using CastZone.Tools.Aspect;

namespace CastZone.Importer.WordsScheduler.Persistences
{
    public class SqlWordPersistence : IWordPersistence
    {
        [Logging]
        public async Task<IEnumerable<Word>> GetWordsAsync() =>
            await Disposable.UsingAsAsync(
                () => new WordContext(), 
                db => db.Word.ToList());
    }
}
