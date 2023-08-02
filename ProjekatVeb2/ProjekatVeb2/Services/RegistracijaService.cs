using AutoMapper;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;
using ProjekatVeb2.Models.Ispis;

namespace ProjekatVeb2.Services
{
    public class RegistracijaService : IRegistracijaService
    {
        private readonly IKorisnikRepository _korisnikRepository;
        private readonly IMapper _mapper;
        private readonly IEnkripcijaService _enkripcijaService;

        public RegistracijaService(IKorisnikRepository korisnikRepository, IMapper mapper, IEnkripcijaService enkripcijaService)
        {
            _korisnikRepository = korisnikRepository;
            _mapper = mapper;
            _enkripcijaService = enkripcijaService;
        }

        public async Task<Registrovanje> RegistrujKorisnika(RegistracijaDTO registracijaDTO)

        {
            Registrovanje rezultat = new Registrovanje { KorisnikRegistrovan = false, Poruka = new List<string>() };


            bool dostupno = await _korisnikRepository.ProvjeriDostupnostKorisnickogImena(registracijaDTO.KorisnickoIme);
            if (dostupno)
            {
                rezultat.Poruka.Add("Korisnicko ime već postoji.");
            }

            bool zauzet = await _korisnikRepository.ProvjeriZauzetostEmail(registracijaDTO.Email);
            if (!zauzet)
            {
                rezultat.Poruka.Add("Email već postoji.");
            }

            if (registracijaDTO.Lozinka.Length < 8)
            {
                rezultat.Poruka.Add("Lozinka mora imati barem 8 karaktera.");
            }

            if (registracijaDTO.Lozinka != registracijaDTO.PonoviLozinku)
            {
                rezultat.Poruka.Add("Lozinke se ne poklapaju.");
            }

            if (rezultat.Poruka.Count > 0)
            {
                return rezultat;
            }


            // Enkripcija lozinke
            string hashLozinke = _enkripcijaService.EnkriptujLozinku(registracijaDTO.Lozinka);


            Korisnik novi = _mapper.Map<RegistracijaDTO, Korisnik>(registracijaDTO);

            if (registracijaDTO.Slika != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    registracijaDTO.Slika.CopyTo(memoryStream);
                    var slikaByte = memoryStream.ToArray();
                    novi.Slika = slikaByte;
                }
            }

            novi.Lozinka = hashLozinke;
            novi.PonoviLozinku = hashLozinke;

            await _korisnikRepository.DodajKorisnika(novi);
            KorisnikDTO korisnikDTO = _mapper.Map<Korisnik, KorisnikDTO>(novi);
            return new Registrovanje { KorisnikRegistrovan = true, Poruka = new List<string> { "Uspjesno registrovan korisnik." }, KorisnikDto = korisnikDTO };

        }
    }
}
