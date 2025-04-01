using BuildWeek_Api.Data;
using BuildWeek_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Services
{
    public class AnimaleServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AnimaleServices> _logger;
        public AnimaleServices(ApplicationDbContext context, ILogger<AnimaleServices> logger)
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
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Animale>> GetAnimaliAsync()
        {
            try
            {
                return await _context.Animali.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Animale> GetAnimaleByIdAsync(Guid id)
        {
            try
            {
                return await _context.Animali.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }         
        }

        public async Task<Animale> CreateAnimaleAsync(Animale animale)
        {
            try
            {
                if (!_context.Clienti.Any(c => c.CodiceFiscale == animale.CodiceFiscaleProprietario))
                {
                    var nuovoCliente = new Cliente
                    {
                        CodiceFiscale = animale.CodiceFiscaleProprietario,
                    };
                    _context.Clienti.Add(nuovoCliente);
                    await _context.SaveChangesAsync();
                }
                _context.Animali.Add(animale);
                if (!await SaveAsync())
                {
                    return null;
                }
                return animale;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
            
        }

        public async Task<bool> UpdateAnimaleAsync(Guid id, Animale animale)
        {
            try
            {
                var animaleEsistente = await GetAnimaleByIdAsync(id);

                if (animaleEsistente == null)
                {
                    _logger.LogWarning("Update fallito: Animale con id {Id} non trovato", id);
                    return false;
                }
                animaleEsistente.NomeAnimale = animale.NomeAnimale;
                animaleEsistente.DataRegistrazione = animale.DataRegistrazione;
                animaleEsistente.Tipologia = animale.Tipologia;
                animaleEsistente.ColoreMantello = animale.ColoreMantello;
                animaleEsistente.DataNascita = animale.DataNascita;
                animaleEsistente.MicrochipPresente = animale.MicrochipPresente;
                animaleEsistente.NumeroMicrochip = animale.NumeroMicrochip;
                animaleEsistente.NomeProprietario = animale.NomeProprietario;
                animaleEsistente.CognomeProprietario = animale.CognomeProprietario;
                animaleEsistente.Visite = animale.Visite;
                animaleEsistente.Ricoveri = animale.Ricoveri;
                animaleEsistente.Vendite = animale.Vendite;
                _logger.LogInformation("Aggiornamento animale {Id}",id);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex, "Errore durante l'aggiornamento dell'animale con id {Id}", id);
                return false;
            }
        }

        public async Task<bool> DeleteAnimaleAsync(Guid id)
        {
            try
            {
                var animale = await GetAnimaleByIdAsync(id);
                if (animale == null)
                    return false;
                _context.Animali.Remove(animale);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }

        }
    }
}
