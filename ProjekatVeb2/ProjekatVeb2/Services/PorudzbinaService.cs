using AutoMapper;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;
using ProjekatVeb2.Repository;

namespace ProjekatVeb2.Services
{
    public class PorudzbinaService :IPorudzbinaService
    {
        private readonly IPorudzbinaRepository _porudzbinaRepozitorijum;
        private readonly IKorisnikRepository _korisnikRepozitorijum;
        private readonly IArtikalRepository _artikalRepozitorijum;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPorudzbinaArtikalRepositroy _porudzbinaArtikalRepozitorijum;

        public PorudzbinaService(IPorudzbinaRepository porudzbinaRepozitorijum, IKorisnikRepository korisnikRepozitorijum, IArtikalRepository artikalRepozitorijum, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPorudzbinaArtikalRepositroy porudzbinaArtikalRepozitorijum)
        {
            _porudzbinaRepozitorijum = porudzbinaRepozitorijum;
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _artikalRepozitorijum = artikalRepozitorijum;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _porudzbinaArtikalRepozitorijum = porudzbinaArtikalRepozitorijum;
        }

        public async Task AzurirajPorudzbinu(PorudzbinaDTO porudzbinaDto)
        {
            bool porudzbinaPostoji = await _porudzbinaRepozitorijum.PorudzbinaPostoji(porudzbinaDto.IdPorudzbine);
            if (!porudzbinaPostoji)
            {
                throw new Exception("Porudzbina sa datim ID-em ne postoji.");
            }
            Porudzbina porudzbina = _mapper.Map<Porudzbina>(porudzbinaDto);
            await _porudzbinaRepozitorijum.AzurirajPorudzbinu(porudzbina);
        }

        public async Task<List<Porudzbina>> DobaviSvePorudzbineKupca(int kupacId)
        {

            var svePorudzbineKupca = await _porudzbinaRepozitorijum.SvePorudzbineKupca(kupacId);


            var trenutnoVreme = DateTime.Now;

            foreach (var porudzbina in svePorudzbineKupca)
            {
                if (porudzbina.Status != StatusPorudzbine.Otkazana && porudzbina.VrijemeDostave < trenutnoVreme)
                {
                    porudzbina.Status = StatusPorudzbine.Isporucena;
                }
            }


            return svePorudzbineKupca;

        }

        public async Task<int> DodajPorudzbinu(KreirajPorudzbinuDTO kreirajPorudzbinuDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");

            if (kreirajPorudzbinuDto == null)
            {
                throw new Exception("Porudzbina ne moze biti null.");
            }
            if (kreirajPorudzbinuDto.AdresaDostave == string.Empty)
            {
                throw new Exception("Morate napisati adresu.");
            }

            if (userIdClaim == null)
            {
                throw new Exception("ID korisnika nije pronađen u claim-u.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new Exception("Nije moguće pretvoriti ID korisnika u broj.");
            }

            var korisnik = await _korisnikRepozitorijum.KorisnikNaOsnovuId(userId);
            Porudzbina porudzbina = _mapper.Map<Porudzbina>(kreirajPorudzbinuDto);


            porudzbina.KorisnikId = userId;
            porudzbina.VrijemeDostave = DateTime.Now.AddHours(1).AddMinutes(new Random().Next(60));
            porudzbina.Status = StatusPorudzbine.UObradi;
            porudzbina.DatumPorudzbine = DateTime.Now;
            porudzbina.Korisnik = korisnik;
            porudzbina.CijenaDostave = 200;



            foreach (var porudzbinaArtikalDto in kreirajPorudzbinuDto.Stavke)
            {
                var artikal = await _artikalRepozitorijum.ArtikalNaOsnovuId(porudzbinaArtikalDto.IdArtikla);
                if (artikal == null)
                {
                    throw new Exception($"Artikal sa ID-em {porudzbinaArtikalDto.IdArtikla} nije pronađen.");
                }

                if (porudzbinaArtikalDto.Kolicina > artikal.Kolicina)
                {
                    throw new Exception("Nema dovoljno artikala.");
                }

                var porudzbinaArtikal = new PoruzdbinaArtikal
                {
                    Porudzbina = porudzbina,
                    Artikal = artikal,
                    Kolicina = porudzbinaArtikalDto.Kolicina
                };

                porudzbina.PorudzbinaArtikal.Add(porudzbinaArtikal);
                artikal.Kolicina -= porudzbinaArtikalDto.Kolicina;
                
                porudzbina.UkupnaCijena += porudzbinaArtikalDto.Kolicina * artikal.Cijena + 200 ;
            }

            await _porudzbinaRepozitorijum.DodajPorudzbinu(porudzbina);

            return porudzbina.IdPorudzbine;
        }


        public async Task<bool> ObrisiPoruzbinu(int id)
        {
            var porudzbina = await _porudzbinaRepozitorijum.PorudzbinaNaOsnovuId(id);
            if (porudzbina != null)
            {
                await _porudzbinaRepozitorijum.ObrisiPorudzbinu(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Porudzbina> PreuzmiPorudzbinuPoId(int id)
        {
            return await _porudzbinaRepozitorijum.PorudzbinaNaOsnovuId(id);

        }

        public async Task<List<Porudzbina>> PreuzmiSvePorudzbine()
        {
            var svePorudzbine = await _porudzbinaRepozitorijum.SvePorudzbine();
            var trenutnoVreme = DateTime.Now;

            foreach (var porudzbina in svePorudzbine)
            {
                if (porudzbina.Status != StatusPorudzbine.Otkazana && porudzbina.VrijemeDostave < trenutnoVreme)
                {
                    porudzbina.Status = StatusPorudzbine.Isporucena;
                }
            }
            return svePorudzbine;
        }

        public async Task OtkaziPorudzbinu(int porudzbinaId)
        {
            var porudzbina = await _porudzbinaRepozitorijum.PorudzbinaNaOsnovuId(porudzbinaId);
            if (porudzbina == null)
            {
                throw new Exception("Porudzbina sa datim ID-em ne postoji.");
            }

            DateTime trenutnoVrijeme = DateTime.Now;
            DateTime datumPorudzbine = porudzbina.DatumPorudzbine;
            TimeSpan protekloVrijeme = trenutnoVrijeme - datumPorudzbine;
            if (protekloVrijeme.TotalHours > 1)
            {
                throw new Exception("Isteklo je vreme za otkazivanje porudzbine.");
            }

            porudzbina.Status = StatusPorudzbine.Otkazana;

            var porudzbinaArtikals = await _porudzbinaArtikalRepozitorijum.PorudzbinaArtikalNaOsnovuPorudzbinaId(porudzbinaId);

            foreach (var porudzbinaArtikal in porudzbinaArtikals)
            {
                var artikal = await _artikalRepozitorijum.ArtikalNaOsnovuId(porudzbinaArtikal.ArtikalID);
                artikal.Kolicina += porudzbinaArtikal.Kolicina;
                await _artikalRepozitorijum.AzurirajKolicinuArtikla(artikal.IdArtikla, artikal.Kolicina);
            }

            await _porudzbinaRepozitorijum.UpdatePorudzbina(porudzbina);
        }


        public async Task<DateTime> PreuzmiVrijemeDostave(int id)
        {
            var porudzbina = await _porudzbinaRepozitorijum.PorudzbinaNaOsnovuId(id);
            if (porudzbina == null)
            {
                throw new Exception("Porudzbina sa datim ID-em ne postoji.");
            }

            return porudzbina.VrijemeDostave;
        }

        public async Task<List<Porudzbina>> DobaviPrethodnePorudzbineKupca(int kupacId)
        {
            var prethodnePorudzbineKupca = await _porudzbinaRepozitorijum.SvePrethodnePorudzbineKupca(kupacId);

            var trenutnoVreme = DateTime.Now;

            foreach (var porudzbina in prethodnePorudzbineKupca)
            {
                if (porudzbina.Status != StatusPorudzbine.Otkazana && porudzbina.VrijemeDostave < trenutnoVreme)
                {
                    porudzbina.Status = StatusPorudzbine.Isporucena;
                }
            }

            return prethodnePorudzbineKupca;
        }

        public async Task<List<Porudzbina>> DobaviMojePorudzbineProdavca(int prodavacId)
        {
            var mojePorudzbine = await _porudzbinaRepozitorijum.MojePorudzbineProdavac(prodavacId);
            var trenutnoVreme = DateTime.Now;

            foreach (var porudzbina in mojePorudzbine)
            {
                if (porudzbina.Status != StatusPorudzbine.Otkazana && porudzbina.VrijemeDostave < trenutnoVreme)
                {
                    porudzbina.Status = StatusPorudzbine.Isporucena;
                }
            }
            return mojePorudzbine;
        }

        public async Task<List<Porudzbina>> DobaviNovePorudzbineProdavac(int prodavacId)
        {
            var novePorudzbine = await _porudzbinaRepozitorijum.NovePorudzbineProdavac(prodavacId);
            return novePorudzbine;
        }

        public async Task<List<Artikal>> DobaviArtiklePorudzbine(int porudzbinaId)
        {
            return await _porudzbinaRepozitorijum.DobaviArtiklePorudzbine(porudzbinaId);
        }

        public async Task<List<Artikal>> DobaviArtiklePorudzbineZaProdavca(int porudzbinaId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");

            if (userIdClaim == null)
            {
                throw new Exception("ID korisnika nije pronađen u claim-u.");
            }

            if (!int.TryParse(userIdClaim.Value, out int prodavacId))
            {
                throw new Exception("Nije moguće pretvoriti ID korisnika u broj.");
            }

            List<Artikal> artikli = await _porudzbinaRepozitorijum.DobaviArtiklePorudzbineZaProdavca(porudzbinaId, prodavacId);

            return artikli;
        }
    }
}
