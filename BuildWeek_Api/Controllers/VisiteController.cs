using BuildWeek_Api.DTOs;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisiteController : ControllerBase
    {
        private readonly VisitaService _visitaService;

        public VisiteController(VisitaService visitaService)
        {
            _visitaService = visitaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitaDTO>>> GetVisite()
        {
            return Ok(await _visitaService.GetVisiteAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitaDTO>> GetVisita(Guid id)
        {
            var visita = await _visitaService.GetVisitaByIdAsync(id);
            return visita == null ? NotFound() : Ok(visita);
        }

        [HttpPost]
        public async Task<ActionResult<VisitaDTO>> CreateVisita(VisitaDTO dto)
        {
            var creata = await _visitaService.CreateVisitaAsync(dto);
            if (creata == null) return BadRequest();

            return CreatedAtAction(nameof(GetVisita), new { id = creata.VisitaId }, creata);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisita(Guid id, VisitaDTO dto)
        {
            var updated = await _visitaService.UpdateVisitaAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisita(Guid id)
        {
            var deleted = await _visitaService.DeleteVisitaAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // ANAMNESI: tutte le visite di un animale (ordine cronologico inverso)
        [HttpGet("anamnesi/{animaleId}")]
        public async Task<ActionResult<IEnumerable<VisitaDTO>>> GetAnamnesi(Guid animaleId)
        {
            var visite = await _visitaService.GetAnamnesiByAnimaleAsync(animaleId);
            return Ok(visite);
        }
    }
}
