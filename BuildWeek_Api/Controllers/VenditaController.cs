using BuildWeek_Api.Models;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [ApiController]
    [Route("api/farmacia/[controller]")]
    public class VenditaController : ControllerBase
    {
        private readonly VenditaService _venditaService;

        public VenditaController(VenditaService venditaService)
        {
            _venditaService = venditaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendita>>> GetVendite()
        {
            var vendite = await _venditaService.GetVenditeAsync();
            return Ok(vendite);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendita>> GetVendita(Guid id)
        {
            var vendita = await _venditaService.GetVenditaByIdAsync(id);
            if (vendita == null)
                return NotFound();

            return Ok(vendita);
        }

        [HttpPost]
        public async Task<ActionResult<Vendita>> PostVendita(Vendita vendita)
        {
            var nuovaVendita = await _venditaService.CreateVenditaAsync(vendita);
            if (nuovaVendita == null)
                return BadRequest("Errore nella creazione della vendita.");

            return CreatedAtAction(nameof(GetVendita), new { id = nuovaVendita.Id }, nuovaVendita);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendita(Guid id, Vendita vendita)
        {
            var aggiornata = await _venditaService.UpdateVenditaAsync(id, vendita);
            if (!aggiornata)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendita(Guid id)
        {
            var eliminata = await _venditaService.DeleteVenditaAsync(id);
            if (!eliminata)
                return NotFound();

            return NoContent();
        }

        [HttpGet("byCliente/{codiceFiscale}")]
        public async Task<ActionResult<IEnumerable<Vendita>>> GetVenditeByCliente(string codiceFiscale)
        {
            var vendite = await _venditaService.GetVenditeByClienteAsync(codiceFiscale);
            if (!vendite.Any())
                return NotFound("Nessuna vendita trovata per questo cliente.");

            return Ok(vendite);
        }

        [HttpGet("byDate")]
        public async Task<ActionResult<IEnumerable<Vendita>>> GetVenditeByDate([FromQuery] DateTime date)
        {
            var vendite = await _venditaService.GetVenditeByDateAsync(date);
            if (!vendite.Any())
                return NotFound("Nessuna vendita trovata per questa data.");

            return Ok(vendite);
        }
    }
}