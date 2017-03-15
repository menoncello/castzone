using CastZone.Importer.WordsScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Importer.WordsScheduler.Persistences
{
    public interface IWordPersistence
    {
        Task<IQueryable<Word>> GetWordsAsync();
    }
}
