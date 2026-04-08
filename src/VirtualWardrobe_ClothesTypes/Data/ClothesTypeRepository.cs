using VirtualWardrobe_ClothesTypes.Interface;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Data
{
    public class ClothesTypeRepository : IClothesTypeRepository
    {
        private readonly ClothesTypeDbContext _context;

        public ClothesTypeRepository(ClothesTypeDbContext context)
        {
            _context = context;
        }

        public bool Exists(Guid id)
        {
            return _context.ClothesTypes.Any(x => x.Id == id);
        }

        public bool Exists(string name)
        {
            return _context.ClothesTypes.Any(x => x.Name == name);
        }

        public bool Exists(ClothesType clothesType)
        {
            return Exists(clothesType.Id) && Exists(clothesType.Name);
        }

        public ICollection<ClothesType> GetAllClothesTypes()
        {
            return _context.ClothesTypes.ToList();
        }

        public ClothesType GetClothesTypeById(Guid id)
        {
            if (Exists(id))
            {
                return _context.ClothesTypes.First(x => x.Id == id);
            }
            throw new KeyNotFoundException($"Clothes Type with id {id} not found.");
        }

        public void CreateClothesType(ClothesType clothesType)
        {
            if (Exists(clothesType.Id))
            {
                throw new InvalidOperationException($"Clothes Type with id {clothesType.Id} already exists.");
            }
            if (Exists(clothesType.Name))
            {
                throw new InvalidOperationException($"Clothes Type with name {clothesType.Name} already exists.");
            }
            _context.ClothesTypes.Add(clothesType);
            _context.SaveChanges();
            return;
        }

        public void UpdateClothesType(ClothesType clothesType)
        {
            if (Exists(clothesType.Id))
            {
                if(Exists(clothesType.Name))
                {
                    throw new InvalidOperationException($"Clothes Type with name {clothesType.Name} already exists.");
                }
                var found = _context.ClothesTypes.First(x => x.Id == clothesType.Id);
                found = clothesType;
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Clothes Type with id {clothesType.Id} not found.");
        }

        public void DeleteClothesType(Guid id)
        {
            if(Exists(id))
            {
                var found = _context.ClothesTypes.First(x => x.Id == id);
                _context.ClothesTypes.Remove(found);
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Clothes Type with id {id} not found.");
        }
    }
}
