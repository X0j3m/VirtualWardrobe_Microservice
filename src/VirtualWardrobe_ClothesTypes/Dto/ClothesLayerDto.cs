namespace VirtualWardrobe_ClothesTypes.Dto
{
    public record ClothesLayerDto(Guid Id, string Name);

    public record CreateClothesLayerDto(string Name);

    public record UpdateClothesLayerDto(string Name);
}
