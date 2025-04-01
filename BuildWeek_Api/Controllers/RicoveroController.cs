using BuildWeek_Api.DTOs.Ricovero;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RicoveroController : ControllerBase
    {
        private readonly RicoveroService _service;
        private readonly ILogger<RicoveroController> _logger;

        public RicoveroController(RicoveroService service, ILogger<RicoveroController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("tutti")]
        [Authorize(Roles = "Veterinario")]
        public async Task<ActionResult<List<RicoveroReadDto>>> GetTutti()
        {
            var elenco = await _service.GetTuttiRicoveriAsync();
            return Ok(elenco);
        }

        [HttpPost]
        [Authorize(Roles = "Veterinario")]
        public async Task<IActionResult> CreaRicovero([FromBody] RicoveroCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.AddRicoveroAsync(dto);
            if (!success) return StatusCode(500, "Errore nella creazione del ricovero.");

            return Ok("Ricovero creato con successo.");
        }

        [HttpGet("attivi")]
        [Authorize(Roles = "Veterinario")]
        public async Task<ActionResult<List<RicoveroReadDto>>> GetRicoveriAttivi()
        {
            var elenco = await _service.GetRicoveriAttiviAsync();
            return Ok(elenco);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Veterinario")]
        public async Task<ActionResult<RicoveroReadDto>> GetRicovero(Guid id)
        {
            var r = await _service.GetByIdAsync(id);
            if (r == null) return NotFound("Ricovero non trovato.");
            return Ok(r);
        }

        [HttpPatch("{id}/chiudi")]
        [Authorize(Roles = "Veterinario")]
        public async Task<IActionResult> ChiudiRicovero(Guid id, [FromBody] RicoveroUpdateDto dto)
        {
            var success = await _service.ChiudiRicoveroAsync(id, dto.DataFine);
            if (!success) return NotFound("Ricovero non trovato o errore durante la chiusura.");
            return Ok("Ricovero chiuso con successo.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Veterinario")]
        public async Task<IActionResult> ModificaRicovero(Guid id, [FromBody] RicoveroEditDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.EditRicoveroAsync(id, dto);
            if (!success) return NotFound("Ricovero non trovato o errore durante l'aggiornamento.");

            return Ok("Ricovero aggiornato con successo.");
        }

        [HttpGet("ricerca-microchip/{numero}")]
        [AllowAnonymous]
        public async Task<IActionResult> RicercaPerMicrochip(string numero)
        {
            var r = await _service.RicercaMicrochipAsync(numero);
            if (r == null)
                return NotFound(new { messaggio = "Animale non presente tra i ricoverati." });

            return Ok(new
            {
                presente = true,
                tipologia = r.Tipologia,
                colore = r.ColoreMantello,
                descrizione = r.Descrizione
            });
        }
    }

}
