using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IArtikalService
    {
        Task<IEnumerable<Artikal>> PreuzmiSveArtikle();
        Task<Artikal> PreuzmiArtikalPoId(int id);
        Task<IEnumerable<Artikal>> PretraziArtiklePoNazivu(string naziv);
        Task<IEnumerable<Artikal>> PretraziArtiklePoCijeni(int minCijena, int maxCijena);
        Task DodajNoviArtikal(KreirajArtikalDTO artikalDto);
        Task AzurirajArtikal(IzmijeniArtikalDTO izmijeniArtikalDto);
        Task<bool> ObrisiArtikal(int id);
        Task<IEnumerable<Artikal>> DobaviArtiklePorudzbine(int porudzbinaId);
        Task<IEnumerable<Artikal>> DohvatiArtikleProdavca(int prodavacId);


    }
}
