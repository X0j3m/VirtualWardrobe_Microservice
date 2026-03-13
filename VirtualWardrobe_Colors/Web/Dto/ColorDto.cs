namespace VirtualWardrobe_Colors.Web.Dto
{
    public record ColorDto(Guid Id, string Name);

    public record CreateColorDto(string Name);

    public record UpdateColorDto(string Name);
}
