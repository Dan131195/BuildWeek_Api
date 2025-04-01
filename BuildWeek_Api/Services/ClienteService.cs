using BuildWeek_Api.Data;
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

        public async Task<IEnumerable<Cliente>> GetClientiAsync()
        {
            try
            {
                return await _context.Clienti
                    .Include(c => c.Animali)
                    .Include(c => c.Vendite)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero dei clienti.");
                return Enumerable.Empty<Cliente>();
            }
        }

        public async Task<Cliente?> GetClienteByCFAsync(string codiceFiscale)
        {
            try
            {
                return await _context.Clienti
                    .Include(c => c.Animali)
                    .Include(c => c.Vendite)
                    .FirstOrDefaultAsync(c => c.CodiceFiscale == codiceFiscale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero del cliente {codiceFiscale}");
                return null;
            }
        }

        public async Task<Cliente?> CreateClienteAsync(Cliente cliente)
        {
            try
            {
                if (await _context.Clienti.AnyAsync(c => c.CodiceFiscale == cliente.CodiceFiscale))
                {
                    _logger.LogWarning($"Cliente con CF {cliente.CodiceFiscale} esiste già.");
                    return null;
                }

                _context.Clienti.Add(cliente);
                var result = await SaveAsync();
                return result ? cliente : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella creazione del cliente.");
                return null;
            }
        }

        public async Task<bool> DeleteClienteAsync(string codiceFiscale)
        {
            try
            {
                var cliente = await GetClienteByCFAsync(codiceFiscale);
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
