using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Repository
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly ContextDB _contextDB;

        public PorudzbinaRepository(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task AzurirajPorudzbinu(Porudzbina porudzbina)
        {
            _contextDB.Entry(porudzbina).State = EntityState.Modified;
            await _contextDB.SaveChangesAsync();
        }

        public async Task<Porudzbina> PorudzbinaNaOsnovuId(int id)
        {
            return await _contextDB.Porudzbine.FindAsync(id);
        }

        public async Task DodajPorudzbinu(Porudzbina porudzbina)
        {
            await _contextDB.Porudzbine.AddAsync(porudzbina);
            await _contextDB.SaveChangesAsync();
        }

        public async Task ObrisiPorudzbinu(int id)
        {
            var porudzbina = await _contextDB.Porudzbine.FindAsync(id);
            if (porudzbina != null)
            {
                _contextDB.Porudzbine.Remove(porudzbina);
                await _contextDB.SaveChangesAsync();
            }

        }

        public async Task<bool> PorudzbinaPostoji(int id)
        {
            return await _contextDB.Porudzbine.AnyAsync(a => a.IdPorudzbine == id);
        }

        /*
        public Task<List<Porudzbina>> PretraziPorudzbine(string pretragaKriterijum)
        {
            throw new System.NotImplementedException();
        }
        */

        public async Task<List<Porudzbina>> SvePorudzbine()
        {
            return await _contextDB.Porudzbine.ToListAsync();
        }
    }
}
