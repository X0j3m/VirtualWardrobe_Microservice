using Microsoft.AspNetCore.Mvc;
using VirtualWardrobe_Colors.Web.Dto;

namespace VirtualWardrobe_Colors.Web
{
    [ApiController]
    [Route("api/colors")]
    public class ColorsController : ControllerBase
    {
        // In-memory data store for test purposes
        private static List<ColorDto> _colors = new()
        {
            new ColorDto(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Red"),
            new ColorDto(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Green"),
            new ColorDto(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Blue")
        };

        [HttpGet]
        public ActionResult<IEnumerable<ColorDto>> GetColors()
        {
            return Ok(_colors);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ColorDto> GetColor(Guid id)
        {
            var found = _colors.Where(i => i.Id == id).FirstOrDefault();

            if (found != null)
            {
                return Ok(found);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<ColorDto> CreateColor(CreateColorDto createColorDto)
        {
            var created = new ColorDto(Guid.NewGuid(), createColorDto.Name);
            _colors.Add(created);

            if (_colors.Contains(created))
            {
                return CreatedAtAction(nameof(GetColor), new { id = created.Id }, created);
            }
            return BadRequest();
        }

        [HttpPut]
        public ActionResult<ColorDto> UpdateColor(Guid id, UpdateColorDto updateColorDto)
        {
            var found = _colors.Where(i => i.Id == id).FirstOrDefault();
            if (found != null)
            {
                if (updateColorDto.Name != null)
                {
                    found = found with { Name = updateColorDto.Name };
                }

                return CreatedAtAction(nameof(GetColor), new { id = found.Id }, found);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteColor(Guid id)
        {
            var found = _colors.Where(i => i.Id == id).FirstOrDefault();
            if (found != null)
            {
                _colors.Remove(found);
                return NoContent();
            }
            return NotFound();
        }
    }
}
