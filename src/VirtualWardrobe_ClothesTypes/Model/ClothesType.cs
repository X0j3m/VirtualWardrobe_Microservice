using VirtualWardrobe_ClothesTypes.Dto;

namespace VirtualWardrobe_ClothesTypes.Model
{
    public class ClothesType
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required ClothesLayer Layer { get; set; }

        public ClothesTypeDto ToDto()
        {
            return new ClothesTypeDto(this.Id, this.Name, this.Layer.ToString() ?? string.Empty);
        }

        public ClothesType Update(string updateName, ClothesLayer updatelayer)
        {
            this.Name = updateName;
            this.Layer = updatelayer;

            return this;
        }
    }
}
