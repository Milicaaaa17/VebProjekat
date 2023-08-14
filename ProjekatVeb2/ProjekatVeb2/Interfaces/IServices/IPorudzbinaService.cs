using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IPorudzbinaService
    {
        Task<Porudzbina> PreuzmiPorudzbinuPoId(int id);
        Task<List<Porudzbina>> PreuzmiSvePorudzbine();
        Task<int> DodajPorudzbinu(KreirajPorudzbinuDTO kreirajPorudzbinuDto);
        Task AzurirajPorudzbinu(PorudzbinaDTO porudzbinaDto);
        Task<bool> ObrisiPoruzbinu(int id);
        Task<List<Porudzbina>> DobaviSvePorudzbineKupca(int kupacId);
        Task<List<Porudzbina>> DobaviPrethodnePorudzbineKupca(int kupacId);
        Task OtkaziPorudzbinu(int porudzbinaId);
        Task<DateTime> PreuzmiVrijemeDostave(int id);
        Task<List<Porudzbina>> DobaviMojePorudzbineProdavca(int prodavacId);
        Task<List<Porudzbina>> DobaviNovePorudzbineProdavac(int prodavacId);
        Task<List<Artikal>> DobaviArtiklePorudzbine(int porudzbinaId);
        Task<List<Artikal>> DobaviArtiklePorudzbineZaProdavca(int porudzbinaId);
    }
}
