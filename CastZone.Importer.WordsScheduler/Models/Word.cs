using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Importer.WordsScheduler.Models
{
    public class Word
    {
        public Word()
        {
            
        }

        public Word(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
