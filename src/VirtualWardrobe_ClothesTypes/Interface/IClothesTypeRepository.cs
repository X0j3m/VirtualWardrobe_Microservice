using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Interface
{
    public interface IClothesTypeRepository
    {
        bool Exists(Guid id);
        bool Exists(string name);
        bool Exists(ClothesType type);
        ICollection<ClothesType> GetAllClothesTypes();
        ClothesType GetClothesTypeById(Guid id);
        void CreateClothesType(ClothesType clothesType);
        void UpdateClothesType(ClothesType clothesType);
        void DeleteClothesType(Guid id);
    }
}
