using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Data
{
    public class ClothesTypeDbContext : DbContext
    {
        virtual public DbSet<ClothesType> ClothesTypes { get; set; }
        virtual public DbSet<ClothesLayer> ClothesLayers { get; set; }

        public ClothesTypeDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClothesType>()
                .HasIndex(t => t.Name)
                .IsUnique();
            modelBuilder.Entity<ClothesLayer>()
               .HasIndex(l => l.Name)
               .IsUnique();
        }
    }
}
