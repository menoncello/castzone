using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastZone.Importer.WordsScheduler.Models;

namespace CastZone.Importer.WordsScheduler.Persistences
{
    public class SqlWordPersistence : IWordPersistence
    {
        public Task<IQueryable<Word>> GetWordsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
