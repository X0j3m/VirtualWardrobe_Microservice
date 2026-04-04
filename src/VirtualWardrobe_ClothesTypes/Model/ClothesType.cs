using VirtualWardrobe_ClothesTypes.Web.Dto;

namespace VirtualWardrobe_ClothesTypes.Model
{
    public class ClothesType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ClothesLayer Layer { get; set; }

        public ClothesTypeDto ToDto()
        {
            return new ClothesTypeDto(this.Id, this.Name, this.Layer.ToString());
        }

        public ClothesType Update(UpdateClothesTypeDto updateClothesTypeDto)
        {
            this.Name = updateClothesTypeDto.Name == null ? this.Name : updateClothesTypeDto.Name;
            try
            {
                this.Layer = updateClothesTypeDto.Layer == null ? this.Layer : Enum.Parse<ClothesLayer>(updateClothesTypeDto.Layer);
            }
            catch (Exception ex)
            {
                this.Layer = this.Layer;
            }

            return this;
        }
    }
}
