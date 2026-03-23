using VirtualWardrobe_Colors.Data;
using VirtualWardrobe_Colors.Web.Dto;
using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Service
{
    public class ColorsService
    {
        private readonly ColorsRepository _repository;

        public ColorsService(ColorsRepository repository)
        {
            _repository = repository;
        }

        public ICollection<ColorDto> GetColors()
        {
            ICollection<Color> colors = _repository.GetAllColors();
            return colors.Select(c => c.ToDto()).ToList();
        }

        public ColorDto GetColor(Guid id)
        {
            Color color = _repository.GetColorById(id);
            return color.ToDto();
        }

        public ColorDto CreateColor(CreateColorDto createColorDto)
        {
            var color = new Color
            {
                Id = Guid.CreateVersion7(),
                Name = createColorDto.Name,
            };

            _repository.AddColor(color);
            return color.ToDto();

        }

        public ColorDto UpdateColor(Guid id, UpdateColorDto updateColorDto)
        {
            var color = _repository.GetColorById(id);
            var updatedColor = color.Update(updateColorDto);
            try
            {
                _repository.UpdateColor(updatedColor);
                return updatedColor.ToDto();
            }
            catch (KeyNotFoundException ex)
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
            catch (KeyNotFoundException ex)
            {
                return false;
            }
        }
    }
}
