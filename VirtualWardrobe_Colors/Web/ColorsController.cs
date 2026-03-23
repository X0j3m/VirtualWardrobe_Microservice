using Microsoft.AspNetCore.Mvc;
using VirtualWardrobe_Colors.Data;
using VirtualWardrobe_Colors.Model;
using VirtualWardrobe_Colors.Service;
using VirtualWardrobe_Colors.Web.Dto;

namespace VirtualWardrobe_Colors.Web
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorsController : ControllerBase
    {
        private readonly ColorsService _colorsService;

        public ColorsController(ColorsService colorsService)
        {
            _colorsService = colorsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ColorDto>> GetColors()
        {
            var colors = _colorsService.GetColors();
            if (colors == null)
            {
                return NotFound();
            }
            else if (colors.Any())
            {
                return Ok(colors);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ColorDto> GetColor(Guid id)
        {
            var color = _colorsService.GetColor(id);
            if (color == null)
            {
                return NotFound();
            }
            return Ok(color);
        }

        [HttpPost]
        public ActionResult<ColorDto> CreateColor(CreateColorDto createColorDto)
        {
            var color = _colorsService.CreateColor(createColorDto);
            return CreatedAtAction(nameof(GetColor), new { id = color.Id }, color);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ColorDto> UpdateColor(Guid id, UpdateColorDto updateColorDto)
        {
            var color = _colorsService.UpdateColor(id, updateColorDto);
            return CreatedAtAction(nameof(GetColor), new { id = color.Id }, color);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteColor(Guid id)
        {
            var deleted = _colorsService.DeleteColor(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
