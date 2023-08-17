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



        public async Task<List<Porudzbina>> SvePorudzbine()
        {
            return await _contextDB.Porudzbine.ToListAsync();
        }

        public async Task<List<Porudzbina>> SvePorudzbineKupca(int kupacId)
        {
            return await _contextDB.Porudzbine
            .Where(p => p.KorisnikId == kupacId)
            .ToListAsync();
        }

        public async Task UpdatePorudzbina(Porudzbina porudzbina)
        {
            _contextDB.Porudzbine.Update(porudzbina);
            await _contextDB.SaveChangesAsync();
        }

        public async Task<DateTime> VrijemeDostaveZaPorudzbinu(int idPorudzbine)
        {
            var porudzbina = await _contextDB.Porudzbine.FindAsync(idPorudzbine);
            if (porudzbina != null)
            {
                return porudzbina.VrijemeDostave;
            }

            throw new Exception($"Porudzbina  nije pronadjena.");
        }

        public async Task<List<Porudzbina>> SvePrethodnePorudzbineKupca(int kupacId)
        {
            DateTime trenutnoVrijeme = DateTime.Now.AddHours(-1);

            return await _contextDB.Porudzbine
           .Where(p => p.KorisnikId == kupacId && p.Status != StatusPorudzbine.Otkazana && p.DatumPorudzbine <= trenutnoVrijeme)
           .ToListAsync();
        }

        public async Task<List<Porudzbina>> NovePorudzbineProdavac(int prodavacId)
        {
            var trenutnoVriejme = DateTime.Now.AddHours(-1);

            var porudzbine = await _contextDB.PoruzdbinaArtikli
                .Include(pa => pa.Porudzbina)
                .Where(pa => pa.Artikal.KorisnikId == prodavacId && pa.Porudzbina.Status != StatusPorudzbine.Otkazana && pa.Porudzbina.DatumPorudzbine >= trenutnoVriejme)
                .Select(pa => pa.Porudzbina)
                .Distinct()
                .ToListAsync();

            var filtriranePorudzbine = porudzbine.GroupBy(p => p.IdPorudzbine)
                .Select(g => g.First())
                .ToList();

            return filtriranePorudzbine;
        }

        public async Task<List<Porudzbina>> MojePorudzbineProdavac(int prodavacId)
        {
            var porudzbine = await _contextDB.PoruzdbinaArtikli
                .Include(pa => pa.Porudzbina)
                .Where(pa => pa.Artikal.KorisnikId == prodavacId && pa.Porudzbina.Status != StatusPorudzbine.UObradi)
                .Select(pa => pa.Porudzbina)
                .ToListAsync();

            var filtriranePorudzbine = porudzbine.GroupBy(p => p.IdPorudzbine)
               .Select(g => g.First())
               .ToList();

            return filtriranePorudzbine;
        }

        public async Task<List<Artikal>> DobaviArtiklePorudzbine(int porudzbinaId)
        {
            return await _contextDB.PoruzdbinaArtikli
                .Include(pa => pa.Artikal)
                .Where(pa => pa.IdPorudzbina == porudzbinaId)
                .Select(pa => pa.Artikal)
                .ToListAsync();
        }


        public async Task<List<Artikal>> DobaviArtiklePorudzbineZaProdavca(int porudzbinaId, int prodavacId)
        {
            return await _contextDB.PoruzdbinaArtikli
            .Include(pa => pa.Artikal)
            .Where(pa => pa.IdPorudzbina == porudzbinaId && pa.Artikal.KorisnikId == prodavacId)
            .Select(pa => pa.Artikal)
            .ToListAsync();
        }
    }
}
