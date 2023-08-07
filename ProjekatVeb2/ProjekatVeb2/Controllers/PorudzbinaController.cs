using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Controllers
{
    [Route("api/porudzbine")]
    [ApiController]
    public class PorudzbinaController : ControllerBase
    {
        private readonly IPorudzbinaService _porudzbinaService;
        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            _porudzbinaService = porudzbinaService;
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
        public async Task<IActionResult> DodajPorudzbinu([FromBody] Porudzbina porudzbina)
        {
            if (porudzbina == null)
            {
                return BadRequest("Porudzbina ne moze biti null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _porudzbinaService.DodajPorudzbinu(porudzbina);
                return Ok("Uspjesno ste dodali novu porudzbinu");
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AzurirajPorudzbinu(int id, [FromBody] Porudzbina porudzbina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != porudzbina.IdPorudzbine)
            {
                return BadRequest("ID porudzbine se ne podudara sa ID u zahtevu.");
            }

            try
            {
                await _porudzbinaService.AzurirajPorudzbinu(porudzbina);
                return Ok("Porudzbina je uspjesno azurirana.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
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
