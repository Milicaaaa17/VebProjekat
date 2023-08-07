using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IArtikalService
    {
        Task<IEnumerable<Artikal>> PreuzmiSveArtikle();
        Task<Artikal> PreuzmiArtikalPoId(int id);
        Task<IEnumerable<Artikal>> PretraziArtiklePoNazivu(string naziv);
        Task<IEnumerable<Artikal>> PretraziArtiklePoCijeni(double minCijena, double maxCijena);
        Task DodajNoviArtikal(KreirajArtikalDTO artikalDto);
        Task AzurirajArtikal(ArtikalDTO artikalDto);
        Task<bool> ObrisiArtikal(int id);
    }
}
