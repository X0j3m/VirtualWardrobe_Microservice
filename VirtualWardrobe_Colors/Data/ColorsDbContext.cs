using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Data
{
    public class ColorsDbContext : DbContext
    {
        public virtual DbSet<Color> Colors { get; set; }

        public ColorsDbContext(DbContextOptions options) : base(options) {}
    }
}
