namespace VirtualWardrobe_ClothesTypes.Dto
{
    public record ClothesTypeDto(Guid Id, string Name, string Layer);

    public record CreateClothesTypeDto(string Name, Guid LayerId);

    public record UpdateClothesTypeDto(string Name, Guid LayerId);
}
