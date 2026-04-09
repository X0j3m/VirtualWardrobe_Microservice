using VirtualWardrobe_ClothesTypes.Dto;

namespace VirtualWardrobe_ClothesTypes.Model
{
    public record ClothesType
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required ClothesLayer Layer { get; set; }
    }
}
