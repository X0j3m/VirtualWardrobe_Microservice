using System.Data.Entity;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Data
{
    public class ClothesTypeRepository
    {
        private readonly ClothesTypeContext _context;

        public ClothesTypeRepository(ClothesTypeContext context)
        {
            _context = context;
        }

        public IEnumerable<ClothesType> GetAllClothesTypes()
        {
            return _context.ClothesTypes.ToList();
        }

        public ClothesType GetClothesTypeById(Guid id)
        {
            var found = _context.ClothesTypes.FirstOrDefault(x => x.Id == id);
            if (found != null)
            {
                return found;
            }
            throw new KeyNotFoundException($"Clothes Type with id {id} not found.");
        }

        public void AddClothesType(ClothesType clothesType)
        {
            var found = _context.ClothesTypes.FirstOrDefault(x => x.Id == clothesType.Id);
            if (found == null)
            {
                _context.ClothesTypes.Add(clothesType);
                _context.SaveChanges();
                return;
            }
            throw new InvalidOperationException($"Clothes Type with id {clothesType.Id} already exists.");
        }

        public void UpdateClothesType(ClothesType clothesType)
        {
            var found = _context.ClothesTypes.FirstOrDefault(x => x.Id == clothesType.Id);
            if (found != null)
            {
                found = clothesType;
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Clothes Type with id {clothesType.Id} not found.");
        }

        public void DeleteClothesType(Guid id)
        {
            var found = _context.ClothesTypes.FirstOrDefault(x => x.Id == id);
            if (found != null)
            {
                _context.ClothesTypes.Remove(found);
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Clothes Type with id {id} not found.");
        }
    }
}
