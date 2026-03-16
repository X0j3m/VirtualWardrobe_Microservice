using Microsoft.AspNetCore.Mvc;
using VirtualWardrobe_ClothesTypes.Service;
using VirtualWardrobe_ClothesTypes.Web.Dto;

namespace VirtualWardrobe_ClothesTypes.Web
{
    [ApiController]
    [Route("api/clothes-types")]
    public class ClothesTypeController : ControllerBase
    {
        private readonly ClothesTypeService _clothesTypeService;

        public ClothesTypeController(ClothesTypeService clothesTypeService)
        {
            _clothesTypeService = clothesTypeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClothesTypeDto>> GetAllClothesTypes()
        {
            var types = _clothesTypeService.GetAllClothesTypes();
            if (types == null)
            {
                return NotFound();
            }
            else if (types.Any())
            {
                return Ok(types);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ClothesTypeDto> GetClothesTypeById(Guid id)
        {
            var type = _clothesTypeService.GetClothesTypeById(id);
            if (type == null)
            {
                return NotFound();
            }
            return Ok(type);
        }

        [HttpPost]
        public ActionResult<ClothesTypeDto> AddClothesType(CreateClothesTypeDto createClothesTypeDto)
        {
            var type = _clothesTypeService.AddClothesType(createClothesTypeDto);
            if (type == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetClothesTypeById), new { id = type.Id }, type);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ClothesTypeDto> UpdateClothesType(Guid id, UpdateClothesTypeDto updateClothesTypeDto)
        {
            var type = _clothesTypeService.UpdateClothesType(id, updateClothesTypeDto);
            if (type == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetClothesTypeById), new { id = type.Id }, type);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteClothesType(Guid id)
        {
            bool deleted = _clothesTypeService.DeleteClothesType(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
