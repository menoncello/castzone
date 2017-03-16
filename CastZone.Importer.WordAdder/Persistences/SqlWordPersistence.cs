using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastZone.Tools.Pipes;

namespace CastZone.Importer.WordAdder.Persistences
{
    public class SqlWordPersistence : IWordPersistence
    {
        public async Task<bool> ExistsAsync(string word) =>
            await Disposable.UsingAsAsync(
                () => new WordContext(),
                db => db.Word.Any(x => x.Id == word));
        
        public async Task InsertAsync(Word word) =>
            await Disposable.Using(
                () => new WordContext(),
                db => db.AddAndSaveChangesAsync(word));
    }
}
