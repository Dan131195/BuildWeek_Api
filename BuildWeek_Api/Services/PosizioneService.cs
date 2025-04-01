using BuildWeek_Api.Data;
using BuildWeek_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Services
{
    public class PosizioneService
    {
        private readonly ApplicationDbContext _context;

        public PosizioneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Posizione>> GetAllAsync()
        {
            return await _context.Posizioni
                .ToListAsync();
        }

        public async Task<Posizione?> GetByIdAsync(Guid id)
        {
            return await _context.Posizioni.FindAsync(id);
        }

        public async Task<Posizione> CreateAsync(Posizione pos)
        {
            _context.Posizioni.Add(pos);
            await _context.SaveChangesAsync();
            return pos;
        }

        public async Task<bool> UpdateAsync(Posizione pos)
        {
            var existing = await _context.Posizioni.FindAsync(pos.Id);
            if (existing == null) return false;

            existing.CodiceArmadietto = pos.CodiceArmadietto;
            existing.NumeroCassetto = pos.NumeroCassetto;

            _context.Posizioni.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var pos = await _context.Posizioni.FindAsync(id);
            if (pos == null) return false;

            _context.Posizioni.Remove(pos);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
