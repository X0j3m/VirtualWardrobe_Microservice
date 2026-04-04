using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Data
{
    public class ColorsDbContext : DbContext
    {
        public virtual DbSet<Color> Colors { get; set; }

        public ColorsDbContext() { }

        public ColorsDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
