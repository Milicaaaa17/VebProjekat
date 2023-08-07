using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Controllers
{
    [Route("api/porudzbine")]
    [ApiController]
    public class PorudzbinaController : ControllerBase
    {
        private readonly IPorudzbinaService _porudzbinaService;
        private readonly IMapper _mapper;
        public PorudzbinaController(IPorudzbinaService porudzbinaService, IMapper mapper)
        {
            _porudzbinaService = porudzbinaService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> PorudzbinaPoId(int id)
        {
            var porudzbina = await _porudzbinaService.PreuzmiPorudzbinuPoId(id);
            if (porudzbina != null)
            {
                return Ok(porudzbina);
            }
            else
            {
                return NotFound("Nije pronadjena");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SvePorudzbine()
        {
            var porudzbine = await _porudzbinaService.PreuzmiSvePorudzbine();
            return Ok(porudzbine);
        }

        [HttpPost]
        public async Task<IActionResult> DodajPorudzbinu([FromBody] KreirajPorudzbinuDTO kreirajPorudzbinuDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (kreirajPorudzbinuDto == null)
            {
                return BadRequest("Porudzbina ne moze biti null.");
            }

            else
            {

                await _porudzbinaService.DodajPorudzbinu(kreirajPorudzbinuDto);
                return Ok("Uspjesno ste dodali novu porudzbinu");
            }

        }

    


        [HttpPut("{id}")]
        public async Task<IActionResult> AzurirajPorudzbinu(int id, [FromBody] PorudzbinaDTO porudzbinaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != porudzbinaDto.IdPorudzbine)
            {
                return BadRequest("ID porudzbine se ne podudara sa ID u zahtevu.");
            }
            Porudzbina porudzbina = _mapper.Map<Porudzbina>(porudzbinaDto);


            try
            {
                await _porudzbinaService.AzurirajPorudzbinu(porudzbinaDto);
                return Ok("Porudzbina je uspjesno azurirana.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> IzbrisiPorudzbinu(int id)
        {
            var porudzbina = await _porudzbinaService.PreuzmiPorudzbinuPoId(id);
            if (porudzbina != null)
            {
                await _porudzbinaService.ObrisiPoruzbinu(id);
                return Ok("Uspjesno obrisana porudzbina");
            }
            else
            {
                return NotFound("Nije pronadjena");
            }


        }
    }
}
