using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Data
{
    public class ClothesTypeContext : DbContext
    {
        public DbSet<ClothesType> ClothesTypes { get; set; }

        public ClothesTypeContext(DbContextOptions options) : base(options) {}
    }
}
