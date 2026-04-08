using VirtualWardrobe_ClothesTypes.Interface;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Data
{
    public class ClothesLayerRepository : IClothesLayerRepository
    {
        private readonly ClothesTypeDbContext _context;

        public ClothesLayerRepository(ClothesTypeDbContext context)
        {
            _context = context;
        }

        public bool Exists(Guid id)
        {
            return _context.ClothesLayers.Any(x => x.Id == id);
        }

        public bool Exists(string name)
        {
            return _context.ClothesLayers.Any(x => x.Name == name);
        }

        public bool Exists(ClothesLayer layer)
        {
            return Exists(layer.Id) && Exists(layer.Name);
        }

        public ICollection<ClothesLayer> GetAllClothesLayers()
        {
            return _context.ClothesLayers.ToList();
        }

        public ClothesLayer GetClothesLayerById(Guid id)
        {
            if (Exists(id))
            {
                return _context.ClothesLayers.First(x => x.Id == id);
            }
            throw new InvalidOperationException($"ClothesLayer with id {id} does not exist.");
        }

        public void CreateClothesLayer(ClothesLayer layer)
        {
            if (Exists(layer.Id))
            {
                throw new InvalidOperationException($"ClothesLayer with id {layer.Id} already exists.");
            }
            if (Exists(layer.Name))
            {
                throw new InvalidOperationException($"ClothesLayer with name {layer.Name} already exists.");
            }
            _context.ClothesLayers.Add(layer);
            _context.SaveChanges();
            return;
        }

        public void UpdateClothesLayer(ClothesLayer layer)
        {
            if (Exists(layer.Id))
            {
                if (Exists(layer.Name))
                {
                    throw new InvalidOperationException($"ClothesLayer with name {layer.Name} already exists.");
                }
                _context.Update(layer);
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"ClothesLayer with id {layer.Id} does not exist.");
        }

        public void DeleteClothesLayer(Guid id)
        {
            if (Exists(id))
            {
                var found = _context.ClothesLayers.First(x => x.Id == id);
                _context.ClothesLayers.Remove(found);
                _context.SaveChanges();
                return;
            }
            throw new InvalidOperationException($"ClothesLayer with id {id} does not exist.");
        }
    }
}
