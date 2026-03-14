using Microsoft.EntityFrameworkCore;

namespace VirtualWardrobe_Colors.Model
{
    public class ColorsContext : DbContext
    {
        public DbSet<Color> Colors { get; set; }

        public ColorsContext(DbContextOptions options) : base(options) {}
    }
}
