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

        public IEnumerable<ColorDto> GetColors()
        {
            var colors = _repository.GetAllColors().Select(c => c.ToDto());
            return colors;
        }

        public ColorDto GetColor(Guid id)
        {
            var color = _repository.GetColorById(id);
            return color.ToDto();
        }

        public ColorDto CreateColor(CreateColorDto createColorDto)
        {
            var color = new Color
            {
                Id = Guid.NewGuid(),
                Name = createColorDto.Name,
            };
            try
            {
                _repository.AddColor(color);
                return color.ToDto();
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
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
