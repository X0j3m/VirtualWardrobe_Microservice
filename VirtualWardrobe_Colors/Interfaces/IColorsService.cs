using VirtualWardrobe_Colors.Dto;

namespace VirtualWardrobe_Colors.Interfaces
{
    public interface IColorsService
    {
        ICollection<ColorDto> GetColors();
        ColorDto? GetColor(Guid id);
        ColorDto? CreateColor(CreateColorDto createColorDto);
        ColorDto? UpdateColor(Guid id, UpdateColorDto updateColorDto);
        bool DeleteColor(Guid id);
    }
}
