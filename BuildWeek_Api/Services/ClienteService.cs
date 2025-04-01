using BuildWeek_Api.Data;
using BuildWeek_Api.DTOs;
using BuildWeek_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Services
{
    public class ClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(ApplicationDbContext context, ILogger<ClienteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il salvataggio delle modifiche cliente.");
                return false;
            }
        }

        // GET: tutti i clienti come DTO
        public async Task<IEnumerable<ClienteDTO>> GetClientiAsync()
        {
            try
            {
                return await _context.Clienti
                    .Select(c => new ClienteDTO
                    {
                        CodiceFiscale = c.CodiceFiscale
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero dei clienti.");
                return Enumerable.Empty<ClienteDTO>();
            }
        }

        // GET: un singolo cliente come DTO (con relazioni se serve)
        public async Task<ClienteDTO?> GetClienteByCFAsync(string codiceFiscale)
        {
            try
            {
                var cliente = await _context.Clienti
                    .Include(c => c.Animali)
                    .Include(c => c.Vendite)
                    .FirstOrDefaultAsync(c => c.CodiceFiscale == codiceFiscale);

                if (cliente == null)
                    return null;

                return new ClienteDTO
                {
                    CodiceFiscale = cliente.CodiceFiscale
                    // puoi aggiungere anche lista di animali o vendite se vuoi
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero del cliente {codiceFiscale}");
                return null;
            }
        }

        // POST: creazione cliente da DTO
        public async Task<ClienteDTO?> CreateClienteAsync(ClienteDTO dto)
        {
            try
            {
                if (await _context.Clienti.AnyAsync(c => c.CodiceFiscale == dto.CodiceFiscale))
                {

                    _logger.LogWarning("Cliente con CF {CF} esiste già.", dto.CodiceFiscale);

                    return null;
                }

                var cliente = new Cliente
                {
                    CodiceFiscale = dto.CodiceFiscale
                };

                _context.Clienti.Add(cliente);
                var result = await SaveAsync();
                return result ? dto : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella creazione del cliente.");
                return null;
            }
        }

        // DELETE
        public async Task<bool> DeleteClienteAsync(string codiceFiscale)
        {
            try
            {
                var cliente = await _context.Clienti
                    .FirstOrDefaultAsync(c => c.CodiceFiscale == codiceFiscale);

                if (cliente == null)
                    return false;

                _context.Clienti.Remove(cliente);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nell'eliminazione del cliente {codiceFiscale}");
                return false;
            }
        }
    }
}
