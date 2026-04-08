using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Interface
{
    public interface IClothesLayerRepository
    {
        bool Exists(Guid id);
        bool Exists(string name);
        bool Exists(ClothesLayer layer);
        ICollection<ClothesLayer> GetAllClothesLayers();
        ClothesLayer GetClothesLayerById(Guid id);
        void CreateClothesLayer(ClothesLayer layer);
        void UpdateClothesLayer(ClothesLayer layer);
        void DeleteClothesLayer(Guid id);
    }
}
