namespace VirtualWardrobe_ClothesTypes.Web.Dto
{
    public record ClothesTypeDto(Guid Id, string Name, string Layer);

    public record CreateClothesTypeDto(string Name, string Layer);

    public record UpdateClothesTypeDto(string Name, string Layer);
}
