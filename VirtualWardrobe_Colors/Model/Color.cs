using VirtualWardrobe_Colors.Web.Dto;

namespace VirtualWardrobe_Colors.Model
{
    public class Color
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

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
