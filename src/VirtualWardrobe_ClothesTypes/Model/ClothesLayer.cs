using System.Diagnostics.CodeAnalysis;

namespace VirtualWardrobe_ClothesTypes.Model
{
    public record ClothesLayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesType> ClothesTypes { get; set; }      
    }
}
