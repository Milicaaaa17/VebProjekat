using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Services
{
    public class PorudzbinaService :IPorudzbinaService
    {
        private readonly IPorudzbinaRepository _porudzbinaRepository;

        public PorudzbinaService(IPorudzbinaRepository porudzbinaRepository)
        {
            _porudzbinaRepository = porudzbinaRepository;
        }

        public async Task AzurirajPorudzbinu(Porudzbina porudzbina)
        {
            bool porudzbinaPostoji = await _porudzbinaRepository.PorudzbinaPostoji(porudzbina.IdPorudzbine);
            if (!porudzbinaPostoji)
            {
                throw new Exception("Porudzbina sa datim ID-em ne postoji.");
            }

            await _porudzbinaRepository.AzurirajPorudzbinu(porudzbina);
        }

        public async Task DodajPorudzbinu(Porudzbina porudzbina)
        {
            await _porudzbinaRepository.DodajPorudzbinu(porudzbina);
        }

        public async Task<bool> ObrisiPoruzbinu(int id)
        {
            var porudzbina = await _porudzbinaRepository.PorudzbinaNaOsnovuId(id);
            if (porudzbina != null)
            {
                await _porudzbinaRepository.ObrisiPorudzbinu(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Porudzbina> PreuzmiPorudzbinuPoId(int id)
        {
            return await _porudzbinaRepository.PorudzbinaNaOsnovuId(id);
        }

        public async Task<List<Porudzbina>> PreuzmiSvePorudzbine()
        {
            return await _porudzbinaRepository.SvePorudzbine();
        }
    }
}
