namespace CastZone.Importer.WordsScheduler.Persistences
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
