using BuildWeek_Api.DTOs;
using BuildWeek_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientiController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientiController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClienti()
        {
            var clienti = await _clienteService.GetClientiAsync();
            return Ok(clienti);
        }

        [HttpGet("{codiceFiscale}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(string codiceFiscale)
        {
            var cliente = await _clienteService.GetClienteByCFAsync(codiceFiscale);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> PostCliente(ClienteDTO cliente)
        {
            var creato = await _clienteService.CreateClienteAsync(cliente);
            if (creato == null)
                return Conflict("Cliente già esistente o errore durante la creazione.");

            return CreatedAtAction(nameof(GetCliente), new { codiceFiscale = cliente.CodiceFiscale }, cliente);
        }

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
