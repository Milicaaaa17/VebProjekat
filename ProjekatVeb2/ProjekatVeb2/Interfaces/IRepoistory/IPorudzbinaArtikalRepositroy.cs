using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IRepoistory
{
    public interface IPorudzbinaArtikalRepositroy
    {
        void ObrisiPorudzbinuArtikal(PoruzdbinaArtikal porudzbinaArtikal);
        Task<List<PoruzdbinaArtikal>> PorudzbinaArtikalNaOsnovuPorudzbinaId(int porudzbinaId);

    }
}
