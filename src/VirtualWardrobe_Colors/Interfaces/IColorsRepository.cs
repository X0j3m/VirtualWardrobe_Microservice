
using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Interfaces
{
    public interface IColorsRepository
    {
        bool Exists(Guid id);
        bool Exists(string name);
        bool Exists(Color color);
        ICollection<Color> GetAllColors();
        Color GetColorById(Guid id);
        void AddColor(Color color);
        void UpdateColor(Color color);
        void DeleteColor(Guid id);
    }
}
