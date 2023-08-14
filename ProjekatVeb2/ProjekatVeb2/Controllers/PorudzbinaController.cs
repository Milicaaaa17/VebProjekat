using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Controllers
{
    [Route("api/[controller]")]
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
        [Authorize(Roles = "Kupac")]
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

        [HttpGet("svePorudzbine")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SvePorudzbine()
        {
            var porudzbine = await _porudzbinaService.PreuzmiSvePorudzbine();
            return Ok(porudzbine);
        }

        [HttpPost]
        [Authorize(Roles = "Kupac")]

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

            try
            {
                int idPorudzbine = await _porudzbinaService.DodajPorudzbinu(kreirajPorudzbinuDto);
                return Ok(new { PorudzbinaId = idPorudzbine, Message = "Uspjesno ste dodali novu porudzbinu" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Kupac")]
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
        [Authorize(Roles = "Kupac")]

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

        [HttpGet("svePorudzbineKupca/{kupacId}")]
        [Authorize(Roles = "Kupac")]

        public async Task<IActionResult> DobaviSvePorudzbineKupca(int kupacId)
        {
            var svePorudzbineKupca = await _porudzbinaService.DobaviSvePorudzbineKupca(kupacId);

            return Ok(svePorudzbineKupca);
        }


        [HttpGet("prethodnePorudzbineKupca/{kupacId}")]
        [Authorize(Roles = "Kupac")]

        public async Task<IActionResult> DobaviPrethodnePorudzbineKupca(int kupacId)
        {
            var prethodnePorudzbine = await _porudzbinaService.DobaviPrethodnePorudzbineKupca(kupacId);

            return Ok(prethodnePorudzbine);
        }

        [HttpPut("otkazi/{id}")]
        [Authorize(Roles = "Kupac")]

        public async Task<IActionResult> OtkaziPorudzbinu(int id)
        {
            try
            {
                await _porudzbinaService.OtkaziPorudzbinu(id);
                return Ok("Porudzbina je uspjesno otkazana.");
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    message = "Porudzbina se vise ne moze otkazati.",
                    error = ex.Message
                };
                return BadRequest(errorResponse);
            }
        }


        [HttpGet("{id}/vrijemeDostave")]
        [Authorize(Roles = "Kupac")]
        public async Task<IActionResult> PreuzmiVrijemeDostave(int id)
        {
            try
            {
                var vrijemeDostave = await _porudzbinaService.PreuzmiVrijemeDostave(id);
                return Ok(vrijemeDostave);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("mojePorudzbineProdavac/{prodavacId}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<IActionResult> GetMojePorudzbineProdavca(int prodavacId)
        {
            var prethodnePorudzbine = await _porudzbinaService.DobaviMojePorudzbineProdavca(prodavacId);
            return Ok(prethodnePorudzbine);
        }

        [HttpGet("novePorudzbineProdavac/{prodavacId}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<IActionResult> GetNovePorudzbineProdavac(int prodavacId)
        {
            var novePorudzbine = await _porudzbinaService.DobaviNovePorudzbineProdavac(prodavacId);
            return Ok(novePorudzbine);
        }

        [HttpGet("{id}/artikliPorudzbine")]
        [Authorize(Roles = "Administrator, Kupac")]
        public async Task<IActionResult> DobaviArtikleZaPorudzbinu(int id)
        {
            List<Artikal> artikli = await _porudzbinaService.DobaviArtiklePorudzbine(id);

            if (artikli.Count == 0)
            {
                return NotFound("Prazna");
            }

            return Ok(artikli);
        }

        [HttpGet("{porudzbinaId}/artikliProdavca")]
        [Authorize(Roles = "Prodavac")]
        public async Task<IActionResult> DobaviArtiklePorudzbineZaProdavca(int porudzbinaId)
        {
            List<Artikal> artikli = await _porudzbinaService.DobaviArtiklePorudzbineZaProdavca(porudzbinaId);

            if (artikli.Count == 0)
            {
                return NotFound("prazna");
            }

            return Ok(artikli);

        }

        [HttpGet("porudzbine/{id}")]
        [Authorize]
        public async Task<ActionResult<Porudzbina>> PreuzmiPorudzbinuPoId(int id)
        {
            var porudzbina = await _porudzbinaService.PreuzmiPorudzbinuPoId(id);
            if (porudzbina == null)
            {
                return NotFound("Ne postoji");
            }

            return porudzbina;
        }

    }
}
