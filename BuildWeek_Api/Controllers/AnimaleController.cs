using BuildWeek_Api.DTOs;
using BuildWeek_Api.DTOs.Animale;
using BuildWeek_Api.Models;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaleController : ControllerBase
    {
        private readonly AnimaleServices _animaleServices;
        public AnimaleController(AnimaleServices animaleServices)
        {
            _animaleServices = animaleServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimaleDTO>>> GetAnimali()
        {
            var animali = await _animaleServices.GetAnimaliAsync();
            if (animali == null)
                return NotFound();
            return Ok(animali);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimaleDTO>> GetAnimale(Guid id)
        {
            var animale = await _animaleServices.GetAnimaleByIdAsync(id);
            if (animale == null)
                return NotFound();
            return Ok(animale);
        }

        [HttpPost]
        public async Task<ActionResult<AnimaleDTO>> CreateAnimale([FromBody] CreateAnimaleDTO createDto)
        {
            var created = await _animaleServices.CreateAnimaleAsync(createDto);
            if (created == null)
                return BadRequest("Errore durante la creazione dell'animale.");
            return CreatedAtAction(nameof(GetAnimale), new { id = created.AnimaleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimale(Guid id, [FromBody] UpdateAnimaleDTO updateDto)
        {
            var success = await _animaleServices.UpdateAnimaleAsync(id, updateDto);
            if (!success)
                return BadRequest("Errore durante l'aggiornamento dell'animale.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimale(Guid id)
        {
            var success = await _animaleServices.DeleteAnimaleAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}

