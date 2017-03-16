using System.Data.Entity;

namespace CastZone.Importer.WordsScheduler.Persistences
{
    public partial class WordContext : DbContext
    {
        public WordContext()
            : base("name=WordContext")
        {
        }

        public virtual DbSet<Word> Word { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
