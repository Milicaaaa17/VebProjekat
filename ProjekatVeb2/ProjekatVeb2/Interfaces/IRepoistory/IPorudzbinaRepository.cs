using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IRepoistory
{
    public interface IPorudzbinaRepository
    {
        Task<List<Porudzbina>> SvePorudzbine();
        Task<Porudzbina> PorudzbinaNaOsnovuId(int id);
        Task DodajPorudzbinu(Porudzbina porudzbina);
        Task AzurirajPorudzbinu(Porudzbina porudzbina);
        Task ObrisiPorudzbinu(int id);
        Task<bool> PorudzbinaPostoji(int id);
        Task<List<Porudzbina>> SvePorudzbineKupca(int kupacId);
        Task<List<Porudzbina>> SvePrethodnePorudzbineKupca(int kupacId);
        Task UpdatePorudzbina(Porudzbina porudzbina);
        Task<DateTime> VrijemeDostaveZaPorudzbinu(int idPorudzbine);
        Task<List<Porudzbina>> MojePorudzbineProdavac(int prodavacId);
        Task<List<Porudzbina>> NovePorudzbineProdavac(int prodavacId);

        Task<List<Artikal>> DobaviArtiklePorudzbine(int porudzbinaId);
        Task<List<Artikal>> DobaviArtiklePorudzbineZaProdavca(int porudzbinaId, int prodavacId);
    }
}
