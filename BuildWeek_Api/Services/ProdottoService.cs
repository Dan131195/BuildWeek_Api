using BuildWeek_Api.Data;
using BuildWeek_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Services
{
    public class ProdottoService
    {
        private readonly ApplicationDbContext _context;

        public ProdottoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prodotto>> GetAllAsync()
        {
            return await _context.Prodotti.Include(p => p.Posizione).ToListAsync();
        }

        public async Task<Prodotto?> GetByIdAsync(Guid id)
        {
            return await _context.Prodotti.Include(p => p.Posizione)
                .FirstOrDefaultAsync(p => p.ProdottoId == id);
        }

        public async Task<Prodotto> CreateAsync(Prodotto prodotto)
        {
            _context.Prodotti.Add(prodotto);
            await _context.SaveChangesAsync();
            return prodotto;
        }

        public async Task<bool> UpdateAsync(Prodotto prodotto)
        {
            var existing = await _context.Prodotti.FindAsync(prodotto.ProdottoId);
            if (existing == null)
                return false;

            existing.Nome = prodotto.Nome;
            existing.Tipo = prodotto.Tipo;
            existing.DittaFornitrice = prodotto.DittaFornitrice;
            existing.ProdottoUso = prodotto.ProdottoUso;

            if (prodotto.PosizioneId == null)
            {
                var nuovaPosizione = new Posizione
                {
                    Id = Guid.NewGuid(),
                    CodiceArmadietto = prodotto.Posizione?.CodiceArmadietto ?? "NuovoArmadietto",
                    NumeroCassetto = prodotto.Posizione?.NumeroCassetto ?? "NuovoCassetto"
                };

                await _context.Posizioni.AddAsync(nuovaPosizione);
                await _context.SaveChangesAsync();

                existing.PosizioneId = nuovaPosizione.Id;
            }
            else
            {
                existing.PosizioneId = prodotto.PosizioneId;
            }

            _context.Prodotti.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null) return false;

            _context.Prodotti.Remove(prodotto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
