using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Repository
{
    public class ArtikalRepository : IArtikalRepository
    {
        
        private readonly ContextDB _db;

        public ArtikalRepository(ContextDB db)
        {
            _db = db;
        }

        public async Task<Artikal> ArtikalNaOsnovuId(int id)
        {
            return await _db.Artikli.FindAsync(id);
        }



        public async Task<IEnumerable<Artikal>> ArtikalNaOsnovuNaziva(string naziv)
        {
            return await _db.Artikli.Where(a => a.Naziv.Contains(naziv)).ToListAsync();
        }

        public async Task<IEnumerable<Artikal>> ArtikalNaOsnovuCijene(int minCijena, int maxCijena)
        {
            return await _db.Artikli.Where(a => a.Cijena >= minCijena && a.Cijena <= maxCijena).ToListAsync();
        }

        public async Task AzurirajArtikal(Artikal artikal)
        {
            _db.Update(artikal);
            await _db.SaveChangesAsync();
        }

        public async Task DodajArtikal(Artikal artikal)
        {
            if (artikal == null)
            {
                throw new ArgumentNullException(nameof(artikal), "Artikal ne može biti null.");

            }
            _db.Artikli.Add(artikal);
            await _db.SaveChangesAsync();
        }

        public async Task ObrisiArtikal(int id)
        {
            var artikal = await _db.Artikli.FindAsync(id);
            if (artikal != null)
            {
                _db.Artikli.Remove(artikal);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Artikal>> SviArtikli()
        {
            return await _db.Artikli.ToListAsync();
        }

        public async Task<bool> ArtikalPostoji(int id)
        {
            return await _db.Artikli.AnyAsync(a => a.IdArtikla == id);
        }

        public async Task<bool> ArtikalPostojiPoNazivu(string naziv)
        {
            return await _db.Artikli.AnyAsync(a => a.Naziv == naziv);
        }

        public async Task AzurirajKolicinuArtikla(int artikalId, int novaKolicina)
        {
            var artikal = await _db.Artikli.FindAsync(artikalId);
            if (artikal == null)
            {
                throw new Exception("Artikal sa datim ID-em ne postoji.");
            }

            artikal.Kolicina = novaKolicina;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artikal>> SviArtikliProdavca(int prodavacId)
        {
            return await _db.Artikli.Where(a => a.KorisnikId == prodavacId).ToListAsync();
        }
    }
}
