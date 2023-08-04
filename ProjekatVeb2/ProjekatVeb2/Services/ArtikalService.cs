using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Services
{
    public class ArtikalService : IArtikalService

    {
        private readonly IArtikalRepository _artikalRepozitorijum;

        public ArtikalService(IArtikalRepository artikalRepozitorijum)
        {
            _artikalRepozitorijum = artikalRepozitorijum;
        }

        public async Task AzurirajArtikal(Artikal artikal)
        {
            bool artikalPostoji = await _artikalRepozitorijum.ArtikalPostoji(artikal.IdArtikla);
            if (!artikalPostoji)
            {
                throw new Exception("Artikal sa datim id ne postoji");
            }

            await _artikalRepozitorijum.AzurirajArtikal(artikal);
        }

        public async Task DodajNoviArtikal(Artikal artikal)
        {
            bool artikalPostoji = await _artikalRepozitorijum.ArtikalPostojiPoNazivu(artikal.Naziv);
            if (artikalPostoji)
            {
                throw new Exception("Artikal sa datim nazivom vec postoji");
            }
            if (artikal.Cijena < 0)
            {
                throw new Exception("Cijena mora bii veca od 0");
            }

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


        public async Task<IEnumerable<Artikal>> PretraziArtiklePoCijeni(double minCijena, double maxCijena)
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
    }
}
