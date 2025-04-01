using BuildWeek_Api.Data;
using BuildWeek_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Services
{
    public class VenditaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VenditaService> _logger;

        public VenditaService(ApplicationDbContext context, ILogger<VenditaService> logger)
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
                _logger.LogError(ex, "Errore durante il salvataggio delle modifiche.");
                return false;
            }
        }

        public async Task<IEnumerable<Vendita>> GetVenditeAsync()
        {
            try
            {
                return await _context.Vendite
                    .Include(v => v.Animale)
                    .Include(v => v.Prodotto)
                    .Include(v => v.Cliente)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle vendite.");
                return Enumerable.Empty<Vendita>();
            }
        }

        public async Task<Vendita?> GetVenditaByIdAsync(Guid id)
        {
            try
            {
                return await _context.Vendite
                    .Include(v => v.Animale)
                    .Include(v => v.Prodotto)
                    .Include(v => v.Cliente)
                    .FirstOrDefaultAsync(v => v.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante il recupero della vendita con ID {id}.");
                return null;
            }
        }

        public async Task<IEnumerable<Vendita>> GetVenditeByClienteAsync(string codiceFiscale)
        {
            try
            {
                return await _context.Vendite
                    .Include(v => v.Animale)
                    .Include(v => v.Prodotto)
                    .Include(v => v.Cliente)
                    .Where(v => v.CodiceFiscaleCliente == codiceFiscale)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante la ricerca delle vendite per cliente {codiceFiscale}.");
                return Enumerable.Empty<Vendita>();
            }
        }

        public async Task<IEnumerable<Vendita>> GetVenditeByDateAsync(DateTime date)
        {
            try
            {
                return await _context.Vendite
                    .Include(v => v.Animale)
                    .Include(v => v.Prodotto)
                    .Include(v => v.Cliente)
                    .Where(v => v.DataVendita.Date == date.Date)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante la ricerca delle vendite per la data {date}.");
                return Enumerable.Empty<Vendita>();
            }
        }

        public async Task<Vendita?> CreateVenditaAsync(Vendita vendita)
        {
            try
            {
                // Verifica presenza cliente
                if (!_context.Clienti.Any(c => c.CodiceFiscale == vendita.CodiceFiscaleCliente))
                {
                    var nuovoCliente = new Cliente
                    {
                        CodiceFiscale = vendita.CodiceFiscaleCliente
                    };
                    _context.Clienti.Add(nuovoCliente);
                    await _context.SaveChangesAsync();
                }

                vendita.Id = Guid.NewGuid();
                vendita.DataVendita = DateTime.Now;

                _context.Vendite.Add(vendita);
                var result = await SaveAsync();

                return result ? vendita : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della vendita.");
                return null;
            }
        }

        public async Task<bool> UpdateVenditaAsync(Guid id, Vendita vendita)
        {
            try
            {
                var venditaEsistente = await GetVenditaByIdAsync(id);
                if (venditaEsistente == null)
                {
                    _logger.LogWarning("Vendita con ID {Id} non trovata.", id);
                    return false;
                }

                venditaEsistente.AnimaleId = vendita.AnimaleId;
                venditaEsistente.ProdottoId = vendita.ProdottoId;
                venditaEsistente.CodiceFiscaleCliente = vendita.CodiceFiscaleCliente;
                venditaEsistente.NumeroRicetta = vendita.NumeroRicetta;
                venditaEsistente.DataVendita = DateTime.Now;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento della vendita con ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteVenditaAsync(Guid id)
        {
            try
            {
                var vendita = await GetVenditaByIdAsync(id);
                if (vendita == null)
                    return false;

                _context.Vendite.Remove(vendita);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione della vendita con ID {Id}.", id);
                return false;
            }
        }
    }
}