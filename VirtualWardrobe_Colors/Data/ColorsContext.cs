using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Data
{
    public class ColorsContext : DbContext
    {
        public DbSet<Color> Colors { get; set; }

        public ColorsContext(DbContextOptions options) : base(options) {}
    }
}
