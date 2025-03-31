using System.Text.Json;
using BuildWeek_Api.Models;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaleController : ControllerBase
    {
        private readonly AnimaleServices _animaleService;
        private readonly ILogger<AnimaleController> _logger;
        public AnimaleController(AnimaleServices animaleService, ILogger<AnimaleController> logger)
        {
            _animaleService = animaleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animale>>> GetAnimali()
        {
            var animali = await _animaleService.GetAnimaliAsync();
            if (animali == null)
            {
                return BadRequest(new
                {
                    message = "Qualcosa è andato storto!"
                });
            }

            if (!animali.Any())
            {
                return NoContent();
            }

            var count = animali.Count();

            var text = count == 1 ? $"{count} animale trovato" : $"{count} animali trovati";

            _logger.LogInformation($"Requesting customers info: {JsonSerializer.Serialize(animali, new JsonSerializerOptions() { WriteIndented = true })}");

            return Ok(new
            {
                message = text,
                products = animali
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animale>> GetAnimale(Guid id)
        {
            var animale = await _animaleService.GetAnimaleByIdAsync(id);
            if (animale == null)
                return NotFound();
            return Ok(animale);
        }

        [HttpPost]
        public async Task<ActionResult<Animale>> CreateAnimale(Animale animale)
        {
            var created = await _animaleService.CreateAnimaleAsync(animale);
            if (created == null)
            {
                return BadRequest(new
                {
                    message = "Qualcosa è andato storto!"
                });
            }

            return Ok(new
            {
                message = "Animale aggiunto con successo!"
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAnimale(Guid id,[FromBody] Animale animale)
        {
            var result = await _animaleService.UpdateAnimaleAsync(id, animale);
            return result ? Ok(new { message = "Animale aggionato!" }) : BadRequest(new { message = "qualcosa è andato storto" });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAnimale(Guid id)
        {
            var result = await _animaleService.DeleteAnimaleAsync(id);

            return result ? Ok(new { message = "Animale cancellato" }) : BadRequest(new { message = "qualcosa è andato storto" });
        }
    }
}
