using BuildWeek_Api.Models;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientiController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientiController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clienti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienti()
        {
            var clienti = await _clienteService.GetClientiAsync();
            return Ok(clienti);
        }

        // GET: api/Clienti/{codiceFiscale}
        [HttpGet("{codiceFiscale}")]
        public async Task<ActionResult<Cliente>> GetCliente(string codiceFiscale)
        {
            var cliente = await _clienteService.GetClienteByCFAsync(codiceFiscale);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST: api/Clienti
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            var creato = await _clienteService.CreateClienteAsync(cliente);
            if (creato == null)
                return Conflict("Cliente già esistente o errore durante la creazione.");

            return CreatedAtAction(nameof(GetCliente), new { codiceFiscale = cliente.CodiceFiscale }, cliente);
        }

        // DELETE: api/Clienti/{codiceFiscale}
        [HttpDelete("{codiceFiscale}")]
        public async Task<IActionResult> DeleteCliente(string codiceFiscale)
        {
            var result = await _clienteService.DeleteClienteAsync(codiceFiscale);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
