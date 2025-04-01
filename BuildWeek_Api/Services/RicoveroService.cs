using BuildWeek_Api.Data;
using BuildWeek_Api.DTOs.Ricovero;
using BuildWeek_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Services
{
    public class RicoveroService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RicoveroService> _logger;

        public RicoveroService(ApplicationDbContext context, ILogger<RicoveroService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddRicoveroAsync(RicoveroCreateDto dto)
        {
            try
            {
                var ricovero = new Ricovero
                {
                    RicoveroId = Guid.NewGuid(),
                    DataInizio = dto.DataInizio,
                    Tipologia = dto.Tipologia,
                    ColoreMantello = dto.ColoreMantello,
                    MicrochipPresente = dto.MicrochipPresente,
                    NumeroMicrochip = dto.NumeroMicrochip,
                    Descrizione = dto.Descrizione,
                    AnimaleId = dto.AnimaleId
                };

                _context.Ricoveri.Add(ricovero);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del ricovero.");
                return false;
            }
        }

        public async Task<List<RicoveroReadDto>> GetTuttiRicoveriAsync()
        {
            try
            {
                var ricoveri = await _context.Ricoveri
                    .Include(r => r.Animale)
                    .OrderByDescending(r => r.DataInizio)
                    .ToListAsync();

                return ricoveri.Select(r => new RicoveroReadDto
                {
                    RicoveroId = r.RicoveroId,
                    DataInizio = r.DataInizio,
                    DataFine = r.DataFine,
                    Tipologia = r.Tipologia,
                    ColoreMantello = r.ColoreMantello,
                    MicrochipPresente = r.MicrochipPresente,
                    NumeroMicrochip = r.NumeroMicrochip,
                    Descrizione = r.Descrizione,
                    AnimaleId = r.AnimaleId,
                    NomeAnimale = r.Animale?.NomeAnimale
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero completo dei ricoveri.");
                throw;
            }
        }

        public async Task<List<RicoveroReadDto>> GetRicoveriAttiviAsync()
        {
            try
            {
                var ricoveri = await _context.Ricoveri
                    .Include(r => r.Animale)
                    .Where(r => r.DataFine == null)
                    .OrderByDescending(r => r.DataInizio)
                    .ToListAsync();

                return ricoveri.Select(r => new RicoveroReadDto
                {
                    RicoveroId = r.RicoveroId,
                    DataInizio = r.DataInizio,
                    DataFine = r.DataFine,
                    Tipologia = r.Tipologia,
                    ColoreMantello = r.ColoreMantello,
                    MicrochipPresente = r.MicrochipPresente,
                    NumeroMicrochip = r.NumeroMicrochip,
                    Descrizione = r.Descrizione,
                    AnimaleId = r.AnimaleId,
                    NomeAnimale = r.Animale?.NomeAnimale
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero dei ricoveri attivi.");
                throw;
            }
        }

        public async Task<RicoveroReadDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var r = await _context.Ricoveri
                    .Include(r => r.Animale)
                    .FirstOrDefaultAsync(r => r.RicoveroId == id);

                if (r == null) return null;

                return new RicoveroReadDto
                {
                    RicoveroId = r.RicoveroId,
                    DataInizio = r.DataInizio,
                    DataFine = r.DataFine,
                    Tipologia = r.Tipologia,
                    ColoreMantello = r.ColoreMantello,
                    MicrochipPresente = r.MicrochipPresente,
                    NumeroMicrochip = r.NumeroMicrochip,
                    Descrizione = r.Descrizione,
                    AnimaleId = r.AnimaleId,
                    NomeAnimale = r.Animale?.NomeAnimale
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero del ricovero ID={id}");
                throw;
            }
        }

        public async Task<bool> ChiudiRicoveroAsync(Guid id, DateTime dataFine)
        {
            try
            {
                var ricovero = await _context.Ricoveri.FindAsync(id);
                if (ricovero == null) return false;

                ricovero.DataFine = dataFine;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nella chiusura del ricovero ID={id}");
                return false;
            }
        }

        public async Task<bool> EditRicoveroAsync(Guid id, RicoveroEditDto dto)
        {
            try
            {
                var ricovero = await _context.Ricoveri.FindAsync(id);
                if (ricovero == null) return false;

                ricovero.DataInizio = dto.DataInizio;
                ricovero.DataFine = dto.DataFine;
                ricovero.Tipologia = dto.Tipologia;
                ricovero.ColoreMantello = dto.ColoreMantello;
                ricovero.MicrochipPresente = dto.MicrochipPresente;
                ricovero.NumeroMicrochip = dto.NumeroMicrochip;
                ricovero.Descrizione = dto.Descrizione;
                ricovero.AnimaleId = dto.AnimaleId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante la modifica del ricovero ID={id}");
                return false;
            }
        }

        public async Task<RicoveroReadDto?> RicercaMicrochipAsync(string microchip)
        {
            try
            {
                var r = await _context.Ricoveri
                    .FirstOrDefaultAsync(r => r.NumeroMicrochip == microchip && r.DataFine == null);

                if (r == null) return null;

                return new RicoveroReadDto
                {
                    RicoveroId = r.RicoveroId,
                    DataInizio = r.DataInizio,
                    Tipologia = r.Tipologia,
                    ColoreMantello = r.ColoreMantello,
                    Descrizione = r.Descrizione
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella ricerca microchip.");
                throw;
            }
        }
    }

}
