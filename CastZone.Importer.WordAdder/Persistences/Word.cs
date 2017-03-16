using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastZone.Importer.WordAdder.Persistences
{
    [Table("Word")]
    public partial class Word
    {
        public Word()
        {
            
        }

        public Word(string word)
        {
            Id = word;
        }

        [Key]
        [Column("Word")]
        [StringLength(50)]
        public string Id { get; set; }
    }
}
