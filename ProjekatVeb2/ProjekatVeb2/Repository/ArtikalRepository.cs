using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Repository
{
    public class ArtikalRepository : IArtikalRepository
    {
        private readonly ContextDB _contextDB;

        public ArtikalRepository(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task<Artikal> ArtikalNaOsnovuId(int id)
        {
            return await _contextDB.Artikli.FindAsync(id);
        }

       

        public async Task<IEnumerable<Artikal>> ArtikalNaOsnovuNaziva(string naziv)
        {
            return await _contextDB.Artikli.Where(a => a.Naziv.Contains(naziv)).ToListAsync();
        }

        public async Task<IEnumerable<Artikal>> ArtikalNaOsnovuCijene(double minCijena, double maxCijena)
        {
            return await _contextDB.Artikli.Where(a => a.Cijena >= minCijena && a.Cijena <= maxCijena).ToListAsync();
        }

        public async Task AzurirajArtikal(Artikal artikal)
        {
            _contextDB.Entry(artikal).State = EntityState.Modified;
            await _contextDB.SaveChangesAsync();
        }

        public async Task DodajArtikal(Artikal artikal)
        {
            _contextDB.Artikli.Add(artikal);
            await _contextDB.SaveChangesAsync();
        }

        public async Task ObrisiArtikal(int id)
        {
            var artikal = await _contextDB.Artikli.FindAsync(id);
            if (artikal != null)
            {
                _contextDB.Artikli.Remove(artikal);
                await _contextDB.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Artikal>> SviArtikli()
        {
            return await _contextDB.Artikli.ToListAsync();
        }

        public async Task<bool> ArtikalPostoji(int id)
        {
            return await _contextDB.Artikli.AnyAsync(a => a.IdArtikla == id);
        }

        public async Task<bool> ArtikalPostojiPoNazivu(string naziv)
        {
            return await _contextDB.Artikli.AnyAsync(a => a.Naziv == naziv);
        }

        
    }
}
