using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IRepoistory
{
    public interface IArtikalRepository
    {

        Task<IEnumerable<Artikal>> SviArtikli(); 
        Task<IEnumerable<Artikal>> SviArtikliProdavca(int prodavacId);
        Task<Artikal> ArtikalNaOsnovuId(int id);  
        Task<IEnumerable<Artikal>> ArtikalNaOsnovuNaziva(string naziv); 
        Task<IEnumerable<Artikal>> ArtikalNaOsnovuCijene(int minCijena, int maxCijena); 
        Task DodajArtikal(Artikal artikal); 
        Task AzurirajArtikal(Artikal artikal);
        Task ObrisiArtikal(int id); 
        Task<bool> ArtikalPostoji(int id);
        Task<bool> ArtikalPostojiPoNazivu(string naziv);
        Task AzurirajKolicinuArtikla(int artikalId, int novaKolicina);

    }
}
