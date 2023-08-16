using AutoMapper;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Services
{
    public class ArtikalService : IArtikalService

    {
        private readonly IArtikalRepository _artikalRepozitorijum;
        private readonly IKorisnikRepository _korisnikRepozitorijum;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPorudzbinaArtikalRepositroy _porudzbinaArtikalRepozitorijum;

        public ArtikalService(IArtikalRepository artikalRepozitorijum, IKorisnikRepository korisnikRepozitorijum, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPorudzbinaArtikalRepositroy porudzbinaArtikalRepozitorijum)
        {
            _artikalRepozitorijum = artikalRepozitorijum;
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _porudzbinaArtikalRepozitorijum = porudzbinaArtikalRepozitorijum;
        }

        public async Task DodajNoviArtikal(KreirajArtikalDTO kreirajArtikalDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");
            if (userIdClaim == null)
            {
                throw new Exception("ID korisnika nije pronađen u claim-u.");
            }

            if (!int.TryParse(userIdClaim.Value, out int id))
            {
                throw new Exception("Nije moguće pretvoriti ID korisnika u broj.");
            }
            bool artikalPostoji = await _artikalRepozitorijum.ArtikalPostojiPoNazivu(kreirajArtikalDto.Naziv);
            if (artikalPostoji)
            {
                throw new Exception("Artikal sa istim nazivom vec postoji");
            }
            if (kreirajArtikalDto.Cijena < 0)
            {
                throw new Exception("Cijena artikla mora biti pozitivan broj");
            }

            var korisnik = await _korisnikRepozitorijum.KorisnikNaOsnovuId(id);

            Artikal artikal = _mapper.Map<Artikal>(kreirajArtikalDto);

            using (var ms = new MemoryStream())
            {
                kreirajArtikalDto.Slika.CopyTo(ms);
                var fotografijaByte = ms.ToArray();
                artikal.Slika = fotografijaByte;
            }


            artikal.KorisnikId = id;
            artikal.Korisnik = korisnik;
            await _artikalRepozitorijum.DodajArtikal(artikal);
        }

        public async Task<bool> ObrisiArtikal(int id)
        {
            var artikal = await _artikalRepozitorijum.ArtikalNaOsnovuId(id);
            if (artikal != null)
            {
                await _artikalRepozitorijum.ObrisiArtikal(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Artikal>> PretraziArtiklePoCijeni(int minCijena, int maxCijena)
        {
            return await _artikalRepozitorijum.ArtikalNaOsnovuCijene(minCijena, maxCijena);
        }

        public async Task<IEnumerable<Artikal>> PretraziArtiklePoNazivu(string naziv)
        {
            bool artikalPostoji = await _artikalRepozitorijum.ArtikalPostojiPoNazivu(naziv);
            if (!artikalPostoji)
            {
                return Enumerable.Empty<Artikal>();
            }

            return await _artikalRepozitorijum.ArtikalNaOsnovuNaziva(naziv);
        }

        public async Task<Artikal> PreuzmiArtikalPoId(int id)
        {
            return await _artikalRepozitorijum.ArtikalNaOsnovuId(id);
        }

        public async Task<IEnumerable<Artikal>> PreuzmiSveArtikle()
        {
            return await _artikalRepozitorijum.SviArtikli();
        }

        public async Task<IEnumerable<Artikal>> DobaviArtiklePorudzbine(int porudzbinaId)
        {
            var listaPorudzbinaArtikal = await _porudzbinaArtikalRepozitorijum.PorudzbinaArtikalNaOsnovuPorudzbinaId(porudzbinaId);
            var listaArtikala = new List<Artikal>();

            foreach (var porArt in listaPorudzbinaArtikal)
            {
                var artikal = await _artikalRepozitorijum.ArtikalNaOsnovuId(porArt.ArtikalID);
                listaArtikala.Add(artikal);
            }

            return listaArtikala;
        }


        public async Task<IEnumerable<Artikal>> DohvatiArtikleProdavca(int prodavacId)
        {
            return await _artikalRepozitorijum.SviArtikliProdavca(prodavacId);
        }

        public async Task AzurirajArtikal(IzmijeniArtikalDTO izmijeniArtikalDto)
        {

            if (izmijeniArtikalDto.Kolicina < 0)
            {
                throw new Exception("Kolicina mora biti veca od 0");
            }

            if (izmijeniArtikalDto.Cijena < 0)
            {
                throw new Exception("Cijena mora biti veca od 0");
            }

            Artikal postojeciArtikal = await _artikalRepozitorijum.ArtikalNaOsnovuId(izmijeniArtikalDto.IdArtikla);
            if (postojeciArtikal == null)
            {
                throw new Exception("Artikal ne postoji.");

            }

            postojeciArtikal.Naziv = izmijeniArtikalDto.Naziv;
            postojeciArtikal.Cijena = izmijeniArtikalDto.Cijena;
            postojeciArtikal.Kolicina = izmijeniArtikalDto.Kolicina;
            postojeciArtikal.Opis = izmijeniArtikalDto.Opis;

            if (izmijeniArtikalDto.Slika != null)
            {
                using (var ms = new MemoryStream())
                {
                    izmijeniArtikalDto.Slika.CopyTo(ms);
                    var slikaByte = ms.ToArray();
                    postojeciArtikal.Slika = slikaByte;

                }
            }

            if (izmijeniArtikalDto.Naziv != postojeciArtikal.Naziv)
            {
                throw new Exception("Vec postoji u bazi artikal sa tim  imenom");
            }

            await _artikalRepozitorijum.AzurirajArtikal(postojeciArtikal);
        }
    }
}
