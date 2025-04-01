using BuildWeek_Api.Data;
using BuildWeek_Api.DTOs.Prodotto;
using BuildWeek_Api.Models;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [ApiController]
    [Route("api/farmacia/[controller]")]

    public class ProdottoController : ControllerBase
    {
        private readonly ProdottoService _service;
        private readonly ILogger<ProdottoController> _logger;
        private readonly ApplicationDbContext _context;

        public ProdottoController(ProdottoService service, ILogger<ProdottoController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllProdotti()
        {
            try
            {
                var prodotti = await _service.GetAllAsync();

                var result = prodotti.Select(p => new ProdottoDto
                {
                    ProdottoId = p.ProdottoId,
                    Nome = p.Nome,
                    Tipo = p.Tipo,
                    DittaFornitrice = p.DittaFornitrice,
                    ProdottoUso = p.ProdottoUso,
                    PosizioneId = p.PosizioneId,
                    CodiceArmadietto = p.Posizione?.CodiceArmadietto,
                    NumeroCassetto = p.Posizione?.NumeroCassetto
                }).ToList();

                _logger.LogInformation($"Recuperati {result.Count} prodotti");
                return Ok(new { message =$"Prodotti trovati {result.Count}", data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore in GetAll");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProdottoById(Guid id)
        {
            try
            {
                var prodotto = await _service.GetByIdAsync(id);
                if (prodotto == null)
                {
                    _logger.LogWarning($"Prodotto con ID {id} non trovato");
                    return NotFound();
                }

                var result = new ProdottoDto
                {
                    ProdottoId = prodotto.ProdottoId,
                    Nome = prodotto.Nome,
                    Tipo = prodotto.Tipo,
                    DittaFornitrice = prodotto.DittaFornitrice,
                    ProdottoUso = prodotto.ProdottoUso,
                    PosizioneId = prodotto.PosizioneId,
                    CodiceArmadietto = prodotto.Posizione.CodiceArmadietto,
                    NumeroCassetto = prodotto.Posizione.NumeroCassetto
                };

                _logger.LogInformation($"Recuperato prodotti id {prodotto.ProdottoId}, {prodotto.Nome} - {prodotto.Tipo}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore in GetById per ID {id}");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProdotto([FromBody] ProdottoCreateDto dto)
        {
            try
            {
                var nuovaPosizione = new Posizione
                {
                    Id = Guid.NewGuid(),
                    CodiceArmadietto = dto.CodiceArmadietto,
                    NumeroCassetto = dto.NumeroCassetto
                };

                await _context.Posizioni.AddAsync(nuovaPosizione);
                await _context.SaveChangesAsync();

                //---------

                var prodotto = new Prodotto
                {
                    ProdottoId = Guid.NewGuid(),
                    Nome = dto.Nome,
                    Tipo = dto.Tipo,
                    DittaFornitrice = dto.DittaFornitrice,
                    ProdottoUso = dto.ProdottoUso,
                    PosizioneId = nuovaPosizione.Id
                };

                var created = await _service.CreateAsync(prodotto);

                _logger.LogInformation($"Prodotto creato con ID {created.ProdottoId} e posizione associata");
                return Ok(new
                {
                    message = "Prodotto creato con successo",
                    prodotto = new
                    {
                        created.ProdottoId,
                        created.Nome,
                        created.Tipo,
                        created.DittaFornitrice,
                        created.ProdottoUso
                    },
                    posizione = new
                    {
                        nuovaPosizione.Id,
                        nuovaPosizione.CodiceArmadietto,
                        nuovaPosizione.NumeroCassetto
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del prodotto");
                return StatusCode(500, "Errore interno durante la creazione del prodotto.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProdotto(Guid id, [FromBody] ProdottoUpdateDto dto)
        {
            if (id != dto.ProdottoId)
            {
                _logger.LogWarning($"ID diverso: {id} ≠ body {dto.ProdottoId}");
                return BadRequest("ID mismatch");
            }

            try
            {
                var prodotto = new Prodotto
                {
                    ProdottoId = dto.ProdottoId,
                    Nome = dto.Nome,
                    Tipo = dto.Tipo,
                    DittaFornitrice = dto.DittaFornitrice,
                    ProdottoUso = dto.ProdottoUso,
                    PosizioneId = dto.PosizioneId,
                    Posizione = new Posizione
                    {
                        CodiceArmadietto = dto.CodiceArmadietto,
                        NumeroCassetto = dto.NumeroCassetto
                    }
                };

                var updated = await _service.UpdateAsync(prodotto);
                if (!updated)
                {
                    _logger.LogWarning($"Prodotto con ID {id} non trovato");
                    return NotFound();
                }

                _logger.LogInformation($"Prodotto con ID {id} aggiornato correttamente");
                return Ok(new { message = "Prodotto aggiornato con successo!", data = prodotto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore in Update per ID {id}");
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdotto(Guid id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarning("Prodotto con ID {Id} non trovato per eliminazione", id);
                    return NotFound();
                }

                _logger.LogInformation("Prodotto con ID {Id} eliminato", id);
                return Ok(new { message = "Prodotto eliminato con successo!", id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore in Delete per ID {Id}", id);
                return StatusCode(500, "Qualcosa è andato storto...");
            }
        }
    }
}
