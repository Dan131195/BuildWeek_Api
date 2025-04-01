using BuildWeek_Api.DTOs;
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

        // GET: api/farmacia/Vendita
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenditaDTO>>> GetVendite()
        {
            var vendite = await _venditaService.GetVenditeDtoAsync();
            return Ok(vendite);
        }

        // GET: api/farmacia/Vendita/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VenditaDTO>> GetVendita(Guid id)
        {
            var vendita = await _venditaService.GetVenditaDtoByIdAsync(id);
            if (vendita == null)
                return NotFound();

            return Ok(vendita);
        }

        // POST: api/farmacia/Vendita
        [HttpPost]
        public async Task<ActionResult<VenditaDTO>> PostVendita(VenditaDTO dto)
        {
            var nuovaVendita = await _venditaService.CreateVenditaFromDtoAsync(dto);
            if (nuovaVendita == null)
                return BadRequest("Errore nella creazione della vendita.");

            return CreatedAtAction(nameof(GetVendita), new { id = nuovaVendita.Id }, nuovaVendita);
        }

        // PUT: api/farmacia/Vendita/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendita(Guid id, VenditaDTO dto)
        {
            var aggiornata = await _venditaService.UpdateVenditaFromDtoAsync(id, dto);
            if (!aggiornata)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/farmacia/Vendita/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendita(Guid id)
        {
            var eliminata = await _venditaService.DeleteVenditaAsync(id);
            if (!eliminata)
                return NotFound();

            return NoContent();
        }

        // GET: api/farmacia/Vendita/byCliente/{codiceFiscale}
        [HttpGet("byCliente/{codiceFiscale}")]
        public async Task<ActionResult<IEnumerable<VenditaDTO>>> GetVenditeByCliente(string codiceFiscale)
        {
            var vendite = await _venditaService.GetVenditeDtoByClienteAsync(codiceFiscale);
            if (!vendite.Any())
                return NotFound("Nessuna vendita trovata per questo cliente.");

            return Ok(vendite);
        }

        // GET: api/farmacia/Vendita/byDate?date=yyyy-MM-dd
        [HttpGet("byDate")]
        public async Task<ActionResult<IEnumerable<VenditaDTO>>> GetVenditeByDate([FromQuery] DateTime date)
        {
            var vendite = await _venditaService.GetVenditeDtoByDateAsync(date);
            if (!vendite.Any())
                return NotFound("Nessuna vendita trovata per questa data.");

            return Ok(vendite);
        }
    }
}
