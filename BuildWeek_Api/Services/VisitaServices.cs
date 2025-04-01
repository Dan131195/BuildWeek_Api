using BuildWeek_Api.Data;
using BuildWeek_Api.DTOs;
using BuildWeek_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Services
{
    public class VisitaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VisitaService> _logger;

        public VisitaService(ApplicationDbContext context, ILogger<VisitaService> logger)
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
                _logger.LogError(ex, "Errore nel salvataggio.");
                return false;
            }
        }

        public async Task<IEnumerable<VisitaDTO>> GetVisiteAsync()
        {
            return await _context.Visite
                .Select(v => new VisitaDTO
                {
                    VisitaId = v.VisitaId,
                    DataVisita = v.DataVisita,
                    EsameObiettivo = v.EsameObiettivo,
                    CuraPrescritta = v.CuraPrescritta,
                    AnimaleId = v.AnimaleId
                })
                .ToListAsync();
        }

        public async Task<VisitaDTO?> GetVisitaByIdAsync(Guid id)
        {
            var visita = await _context.Visite.FindAsync(id);
            if (visita == null) return null;

            return new VisitaDTO
            {
                VisitaId = visita.VisitaId,
                DataVisita = visita.DataVisita,
                EsameObiettivo = visita.EsameObiettivo,
                CuraPrescritta = visita.CuraPrescritta,
                AnimaleId = visita.AnimaleId
            };
        }

        public async Task<VisitaDTO?> CreateVisitaAsync(VisitaDTO dto)
        {
            try
            {
                var visita = new Visita
                {
                    VisitaId = Guid.NewGuid(),
                    DataVisita = dto.DataVisita,
                    EsameObiettivo = dto.EsameObiettivo,
                    CuraPrescritta = dto.CuraPrescritta,
                    AnimaleId = dto.AnimaleId
                };

                _context.Visite.Add(visita);
                var success = await SaveAsync();
                if (!success)
                    return null;

                dto.VisitaId = visita.VisitaId;
                return dto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella creazione della visita.");
                return null;
            }
        }

        public async Task<bool> UpdateVisitaAsync(Guid id, VisitaDTO dto)
        {
            var visita = await _context.Visite.FindAsync(id);
            if (visita == null) return false;

            visita.DataVisita = dto.DataVisita;
            visita.EsameObiettivo = dto.EsameObiettivo;
            visita.CuraPrescritta = dto.CuraPrescritta;
            visita.AnimaleId = dto.AnimaleId;

            return await SaveAsync();
        }

        public async Task<bool> DeleteVisitaAsync(Guid id)
        {
            var visita = await _context.Visite.FindAsync(id);
            if (visita == null) return false;

            _context.Visite.Remove(visita);
            return await SaveAsync();
        }

       
        public async Task<IEnumerable<VisitaDTO>> GetAnamnesiByAnimaleAsync(Guid animaleId)
        {
            return await _context.Visite
                .Where(v => v.AnimaleId == animaleId)
                .OrderByDescending(v => v.DataVisita)
                .Select(v => new VisitaDTO
                {
                    VisitaId = v.VisitaId,
                    DataVisita = v.DataVisita,
                    EsameObiettivo = v.EsameObiettivo,
                    CuraPrescritta = v.CuraPrescritta,
                    AnimaleId = v.AnimaleId
                })
                .ToListAsync();
        }
    }
}
