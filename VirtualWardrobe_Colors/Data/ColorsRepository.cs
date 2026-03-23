using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Data
{
    public class ColorsRepository
    {
        private readonly ColorsDbContext _context;

        public ColorsRepository(ColorsDbContext context)
        {
            _context = context;
        }

        public bool Exists(Guid id)
        {
            return _context.Colors.Any(c => c.Id == id);
        }

        public ICollection<Color> GetAllColors()
        {
            return _context.Colors.ToList();
        }

        public Color GetColorById(Guid id)
        {
            if (Exists(id))
            {
                return _context.Colors.First(c => c.Id == id);
            }
            throw new KeyNotFoundException($"Color with id {id} not found.");
        }

        public void AddColor(Color color)
        {
            if (Exists(color.Id))
            {
                throw new InvalidOperationException($"Color with id {color.Id} already exists.");
            }
            _context.Colors.Add(color);
            _context.SaveChanges();
            return;
        }

        public void UpdateColor(Color color)
        {
            if (Exists(color.Id))
            {
                var found = GetColorById(color.Id);
                found.Name = color.Name;
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Color with id {color.Id} not found.");
        }

        public void DeleteColor(Guid id)
        {
            if (Exists(id))
            {
                var found = GetColorById(id);
                _context.Colors.Remove(found);
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Color with id {id} not found.");
        }
    }
}
