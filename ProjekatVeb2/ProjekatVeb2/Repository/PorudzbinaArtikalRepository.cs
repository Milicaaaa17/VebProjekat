using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Repository
{
    public class PorudzbinaArtikalRepository : IPorudzbinaArtikalRepositroy
    {
        private readonly ContextDB _contextDB;

        public PorudzbinaArtikalRepository(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public void ObrisiPorudzbinuArtikal(PoruzdbinaArtikal porudzbinaArtikal)
        {
            _contextDB.PoruzdbinaArtikli.Remove(porudzbinaArtikal);
            _contextDB.SaveChanges();
        }

        public async Task<List<PoruzdbinaArtikal>> PorudzbinaArtikalNaOsnovuPorudzbinaId(int porudzbinaId)
        {
            return await _contextDB.PoruzdbinaArtikli
                .Where(pa => pa.IdPorudzbina == porudzbinaId)
                .ToListAsync();
        }
    }
}
