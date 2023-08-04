using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IRepoistory
{
    public interface IArtikalRepository
    {

        Task<IEnumerable<Artikal>> SviArtikli();
        Task<Artikal> ArtikalNaOsnovuId(int id);
        Task<IEnumerable<Artikal>> ArtikalNaOsnovuNaziva(string naziv);
        Task<IEnumerable<Artikal>> ArtikalNaOsnovuCijene(double minCijena, double maxCijena);
       
        Task DodajArtikal(Artikal artikal);
        Task AzurirajArtikal(Artikal artikal);
        Task ObrisiArtikal(int id);

        Task<bool> ArtikalPostoji(int id);
        Task<bool> ArtikalPostojiPoNazivu(string naziv);
        
    }
}
