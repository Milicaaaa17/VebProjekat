using AutoMapper;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;
using ProjekatVeb2.Repository;

namespace ProjekatVeb2.Services
{
    public class PorudzbinaService :IPorudzbinaService
    {
        private readonly IPorudzbinaRepository _porudzbinaRepository;
        private readonly IMapper _mapper;

        public PorudzbinaService(IPorudzbinaRepository porudzbinaRepository, IMapper mapper)
        {
            _porudzbinaRepository = porudzbinaRepository;
            _mapper = mapper;
        }

        public async Task AzurirajPorudzbinu(PorudzbinaDTO porudzbinaDto)
        {
            bool porudzbinaPostoji = await _porudzbinaRepository.PorudzbinaPostoji(porudzbinaDto.IdPorudzbine);
            if (!porudzbinaPostoji)
            {
                throw new Exception("Porudzbina sa datim ID-em ne postoji.");
            }

            Porudzbina porudzbina = _mapper.Map<Porudzbina>(porudzbinaDto);
            await _porudzbinaRepository.AzurirajPorudzbinu(porudzbina);
        }

       
        public async Task DodajPorudzbinu(KreirajPorudzbinuDTO kreirajPorudzbinuDto)
        {
            Porudzbina porudzbina = _mapper.Map<Porudzbina>(kreirajPorudzbinuDto);
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
