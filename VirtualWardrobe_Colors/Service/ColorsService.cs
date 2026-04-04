using VirtualWardrobe_Colors.Data;
using VirtualWardrobe_Colors.Model;
using VirtualWardrobe_Colors.Dto;
using VirtualWardrobe_Colors.Interfaces;

namespace VirtualWardrobe_Colors.Service
{
    public class ColorsService : IColorsService
    {
        private readonly IColorsRepository _repository;

        public ColorsService(IColorsRepository repository)
        {
            _repository = repository;
        }

        public ICollection<ColorDto> GetColors()
        {
            ICollection<Color> colors = _repository.GetAllColors();
            return colors.Select(c => c.ToDto()).ToList();
        }

        public ColorDto? GetColor(Guid id)
        {
            try
            {
                Color color = _repository.GetColorById(id);
                return color.ToDto();
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public ColorDto? CreateColor(CreateColorDto createColorDto)
        {
            var color = new Color
            {
                Id = Guid.CreateVersion7(),
                Name = createColorDto.Name,
            };
            try
            {
                _repository.AddColor(color);
                return color.ToDto();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public ColorDto? UpdateColor(Guid id, UpdateColorDto updateColorDto)
        {
            try
            {
                var color = _repository.GetColorById(id);
                var updatedColor = color.Update(updateColorDto);
                _repository.UpdateColor(updatedColor);
                return updatedColor.ToDto();
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }

        }

        public bool DeleteColor(Guid id)
        {
            try
            {
                _repository.DeleteColor(id);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}
