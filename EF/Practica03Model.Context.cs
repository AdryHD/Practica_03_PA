using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Practica_03.EF
{
    public partial class Practica03Entities : DbContext
    {
        public Practica03Entities()
            : base("name=Practica03Entities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Abono> Abonos { get; set; }
        public virtual DbSet<Principal> Principal { get; set; }
    }
}
