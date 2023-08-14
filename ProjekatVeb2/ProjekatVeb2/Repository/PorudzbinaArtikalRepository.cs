using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Repository
{
    public class PorudzbinaArtikalRepository : IPorudzbinaArtikalRepositroy
    {
        private readonly ContextDB _db;
        public void ObrisiPorudzbinuArtikal(PoruzdbinaArtikal porudzbinaArtikal)
        {
            _db.PoruzdbinaArtikli.Remove(porudzbinaArtikal);
            _db.SaveChanges();
        }

        public async Task<List<PoruzdbinaArtikal>> PorudzbinaArtikalNaOsnovuPorudzbinaId(int porudzbinaId)
        {
            return await _db.PoruzdbinaArtikli
                .Where(pa => pa.IdPorudzbina == porudzbinaId)
                .ToListAsync();
        }
    }
}
