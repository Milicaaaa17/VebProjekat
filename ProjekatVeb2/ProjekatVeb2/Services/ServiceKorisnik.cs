using ProjekatVeb2.Interfaces;
using ProjekatVeb2.Models;
using ProjekatVeb2.Exceptions;
using System.Text;
using System;
using AutoMapper;
using ProjekatVeb2.DTO;
using Org.BouncyCastle.Crypto.Generators;

namespace ProjekatVeb2.Services
{
    public class ServiceKorisnik : IServiceKorisnik
    {
        private readonly IKorisnikRepository repozitorijumKorisnik;
        private readonly IMapper mapper;
        private readonly IConfiguration konfiguracija;
        private readonly IEmailService emailServis;


        public ServiceKorisnik (IKorisnikRepository repKorisnik, IEmailService emailService, IMapper mapper_, IConfiguration konfiguracija_)
        {
            repozitorijumKorisnik = repKorisnik;
            emailServis = emailService;
            mapper = mapper_;
            konfiguracija = konfiguracija_;
        }
        public async Task<KorisnikDTO> GetById(int id)
        {
            var query = await repozitorijumKorisnik.GetById(id);

            if (query == null)
            {
                throw new Exceptions.NotFound($"Korisnik ID: {id} ne postoji.");
            }

            KorisnikDTO DtoK = mapper.Map<Korisnik, KorisnikDTO>(query);
            return DtoK;
        }




        public async Task<List<KorisnikDTO>> GetSviKorisnici()
        {
            var query = await repozitorijumKorisnik.GetSviKorisnici();

            if (query.Count == 0)
            {
                throw new Exceptions.NotFound($"Nema korisnika!");
            }

            return mapper.Map<List<Korisnik>, List<KorisnikDTO>>(query);
        }


        public async Task<List<KorisnikDTO>> GetSviProdavci()
        {
            var query = await repozitorijumKorisnik.GetSviProdavci();

            List<KorisnikDTO> prodavci = query
                .Where(k => k.Tip == TipKorisnika.Prodavac)
                .Select(k => mapper.Map<Korisnik, KorisnikDTO>(k))
                .ToList();

            if (prodavci.Count == 0)
            {
                throw new Exceptions.NotFound($"Ne postoje prodavci!");
            }

            return prodavci;
        }


        public async Task<KorisnikDTO> Registracija(RegistracijaDTO DtoReg)
        {
            if (String.IsNullOrEmpty(DtoReg.ImePrezime) || String.IsNullOrEmpty(DtoReg.KorisnickoIme) ||
                String.IsNullOrEmpty(DtoReg.Email) || String.IsNullOrEmpty(DtoReg.Adresa) ||
                String.IsNullOrEmpty(DtoReg.Lozinka) || String.IsNullOrEmpty(DtoReg.PotvrdiLozinku) || String.IsNullOrEmpty(DtoReg.Tip.ToString()))
            {
                throw new Exception($"Morate popuniti sva polja!");
            }

            List<Korisnik> korisnici = await repozitorijumKorisnik.GetSviKorisnici();

            if (korisnici.Any(k => k.KorisnickoIme == DtoReg.KorisnickoIme))
            {
                throw new Exceptions.Conflict("Korisničko ime već postoji. Pokušajte ponovo!");
            }

            if (korisnici.Any(u => u.Email == DtoReg.Email))
            {
                throw new Exceptions.Conflict("Email već postoji. Pokušajte ponovo!");
            }

            if (DtoReg.Lozinka != DtoReg.PotvrdiLozinku)
            {
                throw new Exceptions.BadRequest("Lozinke se ne podudaraju. Pokušajte ponovo!");
            }

            Korisnik noviKorisnik = mapper.Map<RegistracijaDTO, Korisnik>(DtoReg);
            noviKorisnik.Slika = Encoding.ASCII.GetBytes(DtoReg.Slika);

            noviKorisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(noviKorisnik.Lozinka);

            if (noviKorisnik.Tip == Models.TipKorisnika.Prodavac)
            {
                noviKorisnik.VerifikacijaKorisnika = Models.Verifikacija.UObradi;
            }
            else
            {
                noviKorisnik.VerifikacijaKorisnika = Models.Verifikacija.Odobren;
            }

            Korisnik registrovaniKorisnik = await repozitorijumKorisnik.Registracija(noviKorisnik);
            KorisnikDTO dtoKor = mapper.Map<Korisnik, KorisnikDTO>(registrovaniKorisnik);
            dtoKor.Slika = Encoding.Default.GetString(noviKorisnik.Slika);
            return dtoKor;
        }




        public async Task<KorisnikDTO> UpdateKorisnik(int id, UpdateKorisnikaDTO KorisnikDTO)
        {
            List<Korisnik> korisnici = await repozitorijumKorisnik.GetSviKorisnici();
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
                throw new Exceptions.NotFound($"Korisnik sa ID-em {id} ne postoji!");

            if (string.IsNullOrWhiteSpace(KorisnikDTO.ImePrezime) ||
                string.IsNullOrWhiteSpace(KorisnikDTO.KorisnickoIme) || string.IsNullOrWhiteSpace(KorisnikDTO.Email) ||
                string.IsNullOrWhiteSpace(KorisnikDTO.Adresa))
                throw new Exceptions.BadRequest("Morate popuniti sva polja za ažuriranje profila!");

            if (KorisnikDTO.KorisnickoIme != korisnik.KorisnickoIme)
                if (korisnici.Any(k => k.KorisnickoIme == KorisnikDTO.KorisnickoIme))
                    throw new Exceptions.Conflict("Korisničko ime već postoji. Pokušajte ponovo!");

            if (KorisnikDTO.Email != korisnik.Email)
                if (korisnici.Any(k => k.Email == KorisnikDTO.Email))
                    throw new Exceptions.Conflict("Email već postoji. Pokušajte ponovo!");

            if (!string.IsNullOrWhiteSpace(KorisnikDTO.NovaLozinka))
            {
                if (string.IsNullOrWhiteSpace(KorisnikDTO.StaraLozinka))
                    throw new Exceptions.BadRequest("Morate unijeti staru lozinku!");

                bool isStaraLozinkaCorrect = BCrypt.Net.BCrypt.Verify(KorisnikDTO.StaraLozinka, korisnik.Lozinka);
                if (!isStaraLozinkaCorrect)
                    throw new Exceptions.BadRequest("Stara lozinka je neispravna!");

                korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(KorisnikDTO.NovaLozinka);
            }

            korisnik.ImePrezime = KorisnikDTO.ImePrezime;

            korisnik.KorisnickoIme = KorisnikDTO.KorisnickoIme;
            korisnik.Email = KorisnikDTO.Email;
            korisnik.Adresa = KorisnikDTO.Adresa;

            if (!string.IsNullOrWhiteSpace(KorisnikDTO.Slika))
                korisnik.Slika = Encoding.ASCII.GetBytes(KorisnikDTO.Slika);

            Korisnik updatedKorisnik = await repozitorijumKorisnik.UpdateKorisnik(korisnik);

            KorisnikDTO dtoK = new KorisnikDTO
            {
                ID = updatedKorisnik.IdK,
                ImePrezime = updatedKorisnik.ImePrezime,
                KorisnickoIme = updatedKorisnik.KorisnickoIme,
                Email = updatedKorisnik.Email,
                Adresa = updatedKorisnik.Adresa,
                //Slika = Encoding.Default.GetString(updatedKorisnik.Slika)
            };

            return dtoK;
        }


        public async Task<KorisnikDTO> OdbijVerifikaciju(int id)
        {
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
            {
                throw new Exceptions.NotFound($"Korisnik sa ID-em: {id} ne postoji.");
            }

            if (korisnik.VerifikacijaKorisnika != Models.Verifikacija.UObradi)
            {
                throw new Exceptions.BadRequest($"Više nije moguće promijeniti verifikaciju!");
            }

            korisnik.VerifikacijaKorisnika = Models.Verifikacija.Odbijen;
            Korisnik noviKorisnik = await repozitorijumKorisnik.UpdateKorisnik(korisnik);

            if (noviKorisnik != null)
            {
                emailServis.SendeEmail(noviKorisnik.Email, noviKorisnik.VerifikacijaKorisnika.ToString());
            }

            KorisnikDTO korisnikDto = mapper.Map<Korisnik, KorisnikDTO>(noviKorisnik);
            return korisnikDto;
        }


        public async Task<KorisnikDTO> Verifikuj(int id)
        {
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
            {
                throw new Exceptions.NotFound($"Korisnik sa ID-em: {id} ne postoji.");
            }

            if (korisnik.VerifikacijaKorisnika != Models.Verifikacija.UObradi)
            {
                throw new Exceptions.BadRequest($"Više nije moguće promijeniti verifikaciju!");
            }

            korisnik.VerifikacijaKorisnika = Models.Verifikacija.Odobren;
            await repozitorijumKorisnik.UpdateKorisnik(korisnik);

            emailServis.SendeEmail(korisnik.Email, korisnik.VerifikacijaKorisnika.ToString());

            return mapper.Map<Korisnik, KorisnikDTO>(korisnik);
        }

    }
}
