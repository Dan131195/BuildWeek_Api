using BuildWeek_Api.Data;
using BuildWeek_Api.Models;
using BuildWeek_Api.DTOs;
using Microsoft.EntityFrameworkCore;
using BuildWeek_Api.DTOs.Animale;

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

        // Mapping: dal dominio al DTO
        private AnimaleDTO MapToDTO(Animale animale)
        {
            if (animale == null)
                return null;

            return new AnimaleDTO
            {
                AnimaleId = animale.AnimaleId,
                DataRegistrazione = animale.DataRegistrazione,
                NomeAnimale = animale.NomeAnimale,
                Tipologia = animale.Tipologia,
                ColoreMantello = animale.ColoreMantello,
                DataNascita = animale.DataNascita,
                MicrochipPresente = animale.MicrochipPresente,
                NumeroMicrochip = animale.NumeroMicrochip,
                NomeProprietario = animale.NomeProprietario,
                CognomeProprietario = animale.CognomeProprietario,
                CodiceFiscaleProprietario = animale.CodiceFiscaleProprietario
            };
        }

        // Mapping: dal DTO di creazione al dominio
        private Animale MapFromCreateDTO(CreateAnimaleDTO dto)
        {
            return new Animale
            {
                DataRegistrazione = dto.DataRegistrazione,
                NomeAnimale = dto.NomeAnimale,
                Tipologia = dto.Tipologia,
                ColoreMantello = dto.ColoreMantello,
                DataNascita = dto.DataNascita,
                MicrochipPresente = dto.MicrochipPresente,
                NumeroMicrochip = dto.NumeroMicrochip,
                NomeProprietario = dto.NomeProprietario,
                CognomeProprietario = dto.CognomeProprietario,
                CodiceFiscaleProprietario = dto.CodiceFiscaleProprietario
            };
        }

        // Mapping: aggiornamento dei campi con i dati del DTO
        private void MapFromUpdateDTO(Animale animale, UpdateAnimaleDTO dto)
        {
            if (dto.AnimaleId != null)
                animale.AnimaleId = dto.AnimaleId;
            if (dto.NomeAnimale != null)
                animale.NomeAnimale = dto.NomeAnimale;
            if (dto.Tipologia != null)
                animale.Tipologia = dto.Tipologia;
            if (dto.ColoreMantello != null)
                animale.ColoreMantello = dto.ColoreMantello;
            if (dto.DataNascita.HasValue)
                animale.DataNascita = dto.DataNascita;
            if (dto.MicrochipPresente.HasValue)
                animale.MicrochipPresente = dto.MicrochipPresente.Value;
            if (dto.NumeroMicrochip != null)
                animale.NumeroMicrochip = dto.NumeroMicrochip;
            if (dto.NomeProprietario != null)
                animale.NomeProprietario = dto.NomeProprietario;
            if (dto.CognomeProprietario != null)
                animale.CognomeProprietario = dto.CognomeProprietario;
        }

        public async Task<IEnumerable<AnimaleDTO>> GetAnimaliAsync()
        {
            try
            {
                var animali = await _context.Animali.ToListAsync();
                return animali.Select(a => MapToDTO(a));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<AnimaleDTO> GetAnimaleByIdAsync(Guid id)
        {
            try
            {
                var animale = await _context.Animali.FindAsync(id);
                return MapToDTO(animale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<AnimaleDTO> CreateAnimaleAsync(CreateAnimaleDTO dto)
        {
            try
            {
                var animale = MapFromCreateDTO(dto);

                // Se il cliente non esiste, crearlo
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
                return MapToDTO(animale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateAnimaleAsync(Guid id, UpdateAnimaleDTO dto)
        {
            try
            {
                var animaleEsistente = await _context.Animali.FindAsync(id);

                if (animaleEsistente == null)
                {
                    _logger.LogWarning("Update fallito: Animale con id {Id} non trovato", id);
                    return false;
                }
                MapFromUpdateDTO(animaleEsistente, dto);
                _logger.LogInformation("Aggiornamento animale {Id}", id);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento dell'animale con id {Id}", id);
                return false;
            }
        }

        public async Task<bool> DeleteAnimaleAsync(Guid id)
        {
            try
            {
                var animale = await _context.Animali.FindAsync(id);
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

