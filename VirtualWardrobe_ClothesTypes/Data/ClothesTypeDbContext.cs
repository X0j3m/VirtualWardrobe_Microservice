using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Data
{
    public class ClothesTypeDbContext : DbContext
    {
        public DbSet<ClothesType> ClothesTypes { get; set; }

        public ClothesTypeDbContext(DbContextOptions options) : base(options) {}
    }
}
