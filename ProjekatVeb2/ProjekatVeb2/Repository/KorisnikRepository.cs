using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Repository
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly ContextDB context;

        public KorisnikRepository(ContextDB _context)
        {
            context = _context;
        }

        public async Task<Korisnik> GetById(int id)
        {
            try
            {
               
                var query = context.Korisnici.Include(k => k.Porudzbine).Where(p => p.IdK == id);
                Korisnik korisnik = await query.FirstOrDefaultAsync();

                return korisnik;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Korisnik>> GetSviKorisnici()
        {
            try
            {
               
                var query = context.Korisnici.Include(p => p.Porudzbine);
                List<Korisnik> korisnici = await query.ToListAsync();

                return korisnici;
            }
            catch (Exception e)
            {
              
                return null;
            }
        }


        public async Task<List<Korisnik>> GetSviProdavci()
        {
            try
            {
               
                var query = context.Korisnici.Include(a => a.Artikili).Where(tk => tk.Tip == TipKorisnika.Prodavac);
                List<Korisnik> prodavci = await query.ToListAsync();

                return prodavci;
            }
            catch (Exception e)
            {
               
                return null;
            }
        }


        public async Task<Korisnik> OdbijVerifikaciju(int id)
        {
            try
            {
                var query = await context.Korisnici.FindAsync(id);

                if (query == null)
                {
                    return null; // Korisnik nije pronađen
                }

                query.VerifikacijaKorisnika = Verifikacija.Odbijen;

                await context.SaveChangesAsync();
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<Korisnik> Registracija(Korisnik korisnik)
        {
            try
            {
                var query = context.Korisnici.Add(korisnik);
                await context.SaveChangesAsync();
                return query.Entity;
            }
            catch (Exception e)
            {
                 return null;
            }
        }

        public async Task<Korisnik> UpdateKorisnik(Korisnik noviKorisnik)
        {
            try
            {
                var query = context.Korisnici.Find(noviKorisnik.IdK);

                if (query == null)
                {
                    return null; // Korisnik nije pronađen
                }

                query.ImePrezime = noviKorisnik.ImePrezime;
                query.Email = noviKorisnik.Email;
                query.Lozinka = noviKorisnik.Lozinka;
                query.Adresa = noviKorisnik.Adresa;
                query.DatumRodjenja = noviKorisnik.DatumRodjenja;
                //  ostala polja koja treba ažurirati

                await context.SaveChangesAsync();
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<Korisnik> Verifikuj(int id)
        {
            try
            {
                var query = await context.Korisnici.FindAsync(id);

                if (query == null)
                {
                    return null; // Korisnik nije pronađen
                }

                query.VerifikacijaKorisnika = Verifikacija.Odobren;

                await context.SaveChangesAsync();
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
