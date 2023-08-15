using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Repository
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly ContextDB _contextDB;
        private readonly IEnkripcijaService _enkripcijaService;

        public KorisnikRepository(ContextDB contextDB, IEnkripcijaService enkripcijaService)
        {
            _contextDB = contextDB;
            _enkripcijaService = enkripcijaService;
        }

        public async Task<bool> ProvjeriDostupnostKorisnickogImena(string korisnickoIme)
        {
            bool dostupno = await _contextDB.Korisnici.AnyAsync(k => k.KorisnickoIme == korisnickoIme);
            return dostupno;
        }


        public async Task<bool> ProvjeriZauzetostEmail(string email)
        {
            bool zauzet = await _contextDB.Korisnici.AnyAsync(k => k.Email == email);
            return !zauzet;
        }


        public async Task<bool> ProvjeriIspravnostEmail(string email)
        {
            bool ispravanEmail = await _contextDB.Korisnici.AnyAsync(k => k.Email == email);
            return ispravanEmail;
        }

        public async Task<bool> ProvjeriIspravnostLozinke(string lozinka)
        {
            // string lozinka1 = _enkripcijaService.EnkriptujLozinku(lozinka);
            bool ispravnaLozinka = await _contextDB.Korisnici.AnyAsync(k => k.Lozinka == lozinka);
            return ispravnaLozinka;
        }

        public async Task<IEnumerable<Korisnik>> SviKorisnici()
        {
            return await _contextDB.Korisnici.ToListAsync();
        }

        public async Task<IEnumerable<Korisnik>> SviProdavci()
        {
            var prodavci = await _contextDB.Korisnici
            .Where(k => k.Tip == TipKorisnika.Prodavac)
            .ToListAsync();
            return prodavci;
        }

        public async Task<Korisnik> KorisnikNaOsnovuId(int id)
        {
            return await _contextDB.Korisnici.FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task AzurirajKorisnika(Korisnik korisnik)
        {
            _contextDB.Update(korisnik);
            await _contextDB.SaveChangesAsync();
        }

        public async Task BrisanjeKorisnikaNaOsnovuId(int id)
        {
            var korisnik = await _contextDB.Korisnici.FirstOrDefaultAsync(k => k.Id == id);
            if (korisnik != null)
            {
                _contextDB.Korisnici.Remove(korisnik);
                await _contextDB.SaveChangesAsync();

            }

        }

        public async Task<bool> AzurirajStatusVerifikacije(int korisnikId, bool verifikovan, StatusVerifikacije statusVerifikacije)
        {
            var korisnik = await _contextDB.Korisnici.FirstOrDefaultAsync(k => k.Id == korisnikId);
            if (korisnik != null && korisnik.Tip == TipKorisnika.Prodavac)
            {
                korisnik.Verifikovan = verifikovan;
                korisnik.StatusVerifikacije = statusVerifikacije;
                await _contextDB.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Korisnik> ProdavacNaOsnovuId(int id)
        {
            return await _contextDB.Korisnici.FirstOrDefaultAsync(k => k.Id == id && k.Tip == TipKorisnika.Prodavac);

        }

        public async Task<IEnumerable<Korisnik>> SviVerifikovaniProdavci()
        {
            var prodavci = await _contextDB.Korisnici
            .Where(k => k.Tip == TipKorisnika.Prodavac && k.Verifikovan && k.StatusVerifikacije == StatusVerifikacije.Odobren)
            .ToListAsync();
            return prodavci;
        }

        public async Task<IEnumerable<Korisnik>> SviProdavciKojiCekajuVerifikaciju()
        {
            var prodavci = await _contextDB.Korisnici
            .Where(k => k.Tip == TipKorisnika.Prodavac && !k.Verifikovan && k.StatusVerifikacije == StatusVerifikacije.UObradi)
            .ToListAsync();
            return prodavci;
        }

        public async Task DodajKorisnika(Korisnik korisnik)
        {

            if (korisnik == null)
            {
                throw new ArgumentNullException(nameof(korisnik), "Korisnik ne može biti null.");

            }
            if (korisnik.Tip == TipKorisnika.Administrator)
            {
                korisnik.StatusVerifikacije = StatusVerifikacije.Odobren;
                korisnik.Verifikovan = true;

            }
            else if (korisnik.Tip == TipKorisnika.Kupac)
            {
                korisnik.StatusVerifikacije = StatusVerifikacije.Odobren;
                korisnik.Verifikovan = true;
            }
            else
            {
                korisnik.StatusVerifikacije = StatusVerifikacije.UObradi;
            }
            _contextDB.Korisnici.Add(korisnik);
            await _contextDB.SaveChangesAsync();
        }

        public async Task<List<Korisnik>> DobaviKorisnike()
        {
            return await _contextDB.Korisnici.ToListAsync();
        }

        public async Task<Korisnik> KorisnikNaOsnovuEmail(string email)
        {
            return await _contextDB.Korisnici.FirstOrDefaultAsync(k => k.Email == email);
        }

        public async Task<IEnumerable<Korisnik>> SviOdbijeniProdavci()
        {
            var prodavci = await _contextDB.Korisnici
            .Where(k => k.Tip == TipKorisnika.Prodavac && k.Verifikovan == false && k.StatusVerifikacije == StatusVerifikacije.Odbijen)
            .ToListAsync();
            return prodavci;
        }

    }
}
