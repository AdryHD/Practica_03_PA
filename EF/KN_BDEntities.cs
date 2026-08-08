using System.Data.Entity;

namespace Practica_03.EF
{
    public partial class KN_BDEntities : DbContext
    {
        public KN_BDEntities()
            : base("name=KN_BDEntities")
        {
        }

        public virtual DbSet<Principal> Principal { get; set; }
        public virtual DbSet<Abono> Abonos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Principal>()
                .Property(entity => entity.Precio)
                .HasPrecision(18, 5);

            modelBuilder.Entity<Principal>()
                .Property(entity => entity.Saldo)
                .HasPrecision(18, 5);

            modelBuilder.Entity<Abono>()
                .Property(entity => entity.Monto)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Principal>()
                .HasMany(entity => entity.Abonos)
                .WithRequired(entity => entity.Principal)
                .HasForeignKey(entity => entity.Id_Compra);
        }
    }
}

