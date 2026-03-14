using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Data
{
    public class ColorsRepository
    {
        private readonly ColorsContext _context;

        public ColorsRepository(ColorsContext context)
        {
            _context = context;
        }

        public IEnumerable<Color> GetAllColors()
        {
            return _context.Colors.ToList();
        }

        public Color GetColorById(Guid id)
        {
            var found = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (found != null)
            {
                return found;                
            }
            throw new KeyNotFoundException($"Color with id {id} not found.");
        }

        public void AddColor(Color color)
        {
            var found = _context.Colors.FirstOrDefault(c => c.Id == color.Id);
            if (found != null)
            {
                _context.Colors.Add(color);
                _context.SaveChanges();
                return;
            }
            throw new InvalidOperationException($"Color with id {color.Id} already exists.");
        }

        public void UpdateColor(Color color)
        {
            var found = _context.Colors.FirstOrDefault(c => c.Id == color.Id);
            if (found != null)
            {
                found.Name = color.Name;
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Color with id {color.Id} not found.");
        }

        public void DeleteColor(Guid id)
        {
            var found = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (found != null)
            {
                _context.Colors.Remove(found);
                _context.SaveChanges();
                return;
            }
            throw new KeyNotFoundException($"Color with id {id} not found.");
        }
    }
}
