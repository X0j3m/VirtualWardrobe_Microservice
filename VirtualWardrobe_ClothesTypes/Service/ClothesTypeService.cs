using VirtualWardrobe_ClothesTypes.Data;
using VirtualWardrobe_ClothesTypes.Model;
using VirtualWardrobe_ClothesTypes.Web.Dto;

namespace VirtualWardrobe_ClothesTypes.Service
{
    public class ClothesTypeService
    {
        private readonly ClothesTypeRepository _repository;

        public ClothesTypeService(ClothesTypeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ClothesTypeDto> GetAllClothesTypes()
        {
            var types = _repository.GetAllClothesTypes().Select(x => x.ToDto());
            return types;
        }

        public ClothesTypeDto GetClothesTypeById(Guid id)
        {
            try
            {
                var type = _repository.GetClothesTypeById(id);
                return type.ToDto();
            }
            catch (KeyNotFoundException ex)
            {
                return null;
            }
        }

        public ClothesTypeDto AddClothesType(CreateClothesTypeDto createClothesTypeDto)
        {
            try
            {
                var type = new ClothesType
                {
                    Id = Guid.NewGuid(),
                    Name = createClothesTypeDto.Name,
                    Layer = Enum.Parse<ClothesLayer>(createClothesTypeDto.Layer)
                };
                _repository.AddClothesType(type);
                return type.ToDto();
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }

        public ClothesTypeDto UpdateClothesType(Guid id, UpdateClothesTypeDto updateClothesTypeDto)
        {
            try
            {
                var found = _repository.GetClothesTypeById(id);
                var updated = found.Update(updateClothesTypeDto);
                _repository.UpdateClothesType(updated);
                return updated.ToDto();
            }
            catch (KeyNotFoundException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteClothesType(Guid id)
        {
            try
            {
                _repository.DeleteClothesType(id);
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                return false;
            }
        }
    }
}
