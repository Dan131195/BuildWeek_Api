using BuildWeek_Api.Data;
using BuildWeek_Api.DTOs;
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
                _logger.LogError(ex, "Errore durante il salvataggio della vendita.");
                return false;
            }
        }

        // 🔹 GET all
        public async Task<IEnumerable<VenditaDTO>> GetVenditeDtoAsync()
        {
            return await _context.Vendite
                .Include(v => v.Animale)
                .Include(v => v.Cliente)
                .Include(v => v.Prodotto)
                .Select(v => new VenditaDTO
                {
                    Id = v.Id,
                    AnimaleId = v.AnimaleId,
                    CodiceFiscaleCliente = v.CodiceFiscaleCliente,
                    ProdottoId = v.ProdottoId,
                    NumeroRicetta = v.NumeroRicetta,
                    DataVendita = v.DataVendita
                })
                .ToListAsync();
        }

        // 🔹 GET by ID
        public async Task<VenditaDTO?> GetVenditaDtoByIdAsync(Guid id)
        {
            var vendita = await _context.Vendite.FindAsync(id);
            if (vendita == null)
                return null;

            return new VenditaDTO
            {
                Id = vendita.Id,
                AnimaleId = vendita.AnimaleId,
                CodiceFiscaleCliente = vendita.CodiceFiscaleCliente,
                ProdottoId = vendita.ProdottoId,
                NumeroRicetta = vendita.NumeroRicetta,
                DataVendita = vendita.DataVendita
            };
        }

        // 🔹 POST
        public async Task<VenditaDTO?> CreateVenditaFromDtoAsync(VenditaDTO dto)
        {
            try
            {
                if (!_context.Clienti.Any(c => c.CodiceFiscale == dto.CodiceFiscaleCliente))
                {
                    _context.Clienti.Add(new Cliente { CodiceFiscale = dto.CodiceFiscaleCliente });
                    await _context.SaveChangesAsync();
                }

                var vendita = new Vendita
                {
                    Id = Guid.NewGuid(),
                    AnimaleId = dto.AnimaleId,
                    CodiceFiscaleCliente = dto.CodiceFiscaleCliente,
                    ProdottoId = dto.ProdottoId,
                    NumeroRicetta = dto.NumeroRicetta,
                    DataVendita = DateTime.Now
                };

                _context.Vendite.Add(vendita);
                var result = await SaveAsync();
                if (!result) return null;

                dto.Id = vendita.Id;
                dto.DataVendita = vendita.DataVendita;
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della vendita.");
                return null;
            }
        }

        // 🔹 PUT
        public async Task<bool> UpdateVenditaFromDtoAsync(Guid id, VenditaDTO dto)
        {
            try
            {
                var vendita = await _context.Vendite.FindAsync(id);
                if (vendita == null)
                    return false;

                vendita.AnimaleId = dto.AnimaleId;
                vendita.CodiceFiscaleCliente = dto.CodiceFiscaleCliente;
                vendita.ProdottoId = dto.ProdottoId;
                vendita.NumeroRicetta = dto.NumeroRicetta;
                vendita.DataVendita = DateTime.Now;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento della vendita.");
                return false;
            }
        }

        // 🔹 DELETE
        public async Task<bool> DeleteVenditaAsync(Guid id)
        {
            try
            {
                var vendita = await _context.Vendite.FindAsync(id);
                if (vendita == null)
                    return false;

                _context.Vendite.Remove(vendita);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione della vendita.");
                return false;
            }
        }

        // 🔹 GET by Cliente
        public async Task<IEnumerable<VenditaDTO>> GetVenditeDtoByClienteAsync(string codiceFiscale)
        {
            return await _context.Vendite
                .Where(v => v.CodiceFiscaleCliente == codiceFiscale)
                .Select(v => new VenditaDTO
                {
                    Id = v.Id,
                    AnimaleId = v.AnimaleId,
                    CodiceFiscaleCliente = v.CodiceFiscaleCliente,
                    ProdottoId = v.ProdottoId,
                    NumeroRicetta = v.NumeroRicetta,
                    DataVendita = v.DataVendita
                })
                .ToListAsync();
        }

        // 🔹 GET by Data
        public async Task<IEnumerable<VenditaDTO>> GetVenditeDtoByDateAsync(DateTime date)
        {
            return await _context.Vendite
                .Where(v => v.DataVendita.Date == date.Date)
                .Select(v => new VenditaDTO
                {
                    Id = v.Id,
                    AnimaleId = v.AnimaleId,
                    CodiceFiscaleCliente = v.CodiceFiscaleCliente,
                    ProdottoId = v.ProdottoId,
                    NumeroRicetta = v.NumeroRicetta,
                    DataVendita = v.DataVendita
                })
                .ToListAsync();
        }
    }
}
