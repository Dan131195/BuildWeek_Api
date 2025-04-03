using BuildWeek_Api.DTOs.Posizione;
using BuildWeek_Api.Models;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [ApiController]
    [Route("api/farmacia/[controller]")]
    [Authorize(Roles = "Veterinario")]
    public class PosizioneController : ControllerBase
    {
        private readonly PosizioneService _service;
        private readonly ILogger<PosizioneController> _logger;

        public PosizioneController(PosizioneService service, ILogger<PosizioneController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> AllPosizioni()
        {
            try
            {
                var posizioni = await _service.GetAllAsync();
                var result = posizioni.Select(p => new PosizioneReadDto
                {
                    Id = p.Id,
                    CodiceArmadietto = p.CodiceArmadietto,
                    NumeroCassetto = p.NumeroCassetto
                }).ToList();

                _logger.LogInformation($"Richieste tutte le posizioni. Trovate: {result.Count}");
                return Ok(new { message = $"Posizioni trovate {result.Count}", data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore in GetAll");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> PosizionetById(Guid id)
        {
            try
            {
                var pos = await _service.GetByIdAsync(id);
                if (pos == null)
                {
                    _logger.LogWarning($"Posizione con ID {id} non trovata");
                    return NotFound();
                }

                return Ok(new PosizioneReadDto
                {
                    Id = pos.Id,
                    CodiceArmadietto = pos.CodiceArmadietto,
                    NumeroCassetto = pos.NumeroCassetto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore in GetById per ID {id}");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePosizione([FromBody] PosizioneCreateDto dto)
        {
            try
            {
                var nuova = new Posizione
                {
                    Id = Guid.NewGuid(),
                    CodiceArmadietto = dto.CodiceArmadietto,
                    NumeroCassetto = dto.NumeroCassetto
                };

                var creata = await _service.CreateAsync(nuova);

                _logger.LogInformation($"Posizione creata con ID {creata.Id}");
                return Ok(new { message = "Posizione Creata con successo!", data = nuova });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore in Create");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosizione(Guid id, [FromBody] PosizioneUpdateDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning($"ID mismatch: route {id} != body {dto.Id}");
                return BadRequest("ID mismatch");
            }

            try
            {
                var pos = new Posizione
                {
                    Id = dto.Id,
                    CodiceArmadietto = dto.CodiceArmadietto,
                    NumeroCassetto = dto.NumeroCassetto
                };

                var success = await _service.UpdateAsync(pos);
                if (!success)
                {
                    _logger.LogWarning($"Posizione con ID {id} non trovata");
                    return NotFound();
                }

                _logger.LogInformation($"Posizione con ID {id} aggiornata");
                return Ok(new { message = "Posizione aggiornata con successo!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore in Update per ID {id}");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosizione(Guid id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    _logger.LogWarning($"Posizione con ID {id} non trovata per eliminazione");
                    return NotFound();
                }

                _logger.LogInformation($"Posizione con ID {id} eliminata");
                return Ok(new { message = "Posizione eliminata con successo!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore in Delete per ID {id}");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }
    }

}
