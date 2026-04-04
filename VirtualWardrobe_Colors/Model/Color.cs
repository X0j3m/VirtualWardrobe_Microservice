using VirtualWardrobe_Colors.Dto;

namespace VirtualWardrobe_Colors.Model
{
    public record Color
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public ColorDto ToDto()
        {
            return new ColorDto(this.Id, this.Name);
        }

        public Color Update (UpdateColorDto updateColorDto)
        {
            this.Name = updateColorDto.Name == null ? this.Name : updateColorDto.Name;
            return this;
        }
    }
}
