using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Controllers
{
    [ApiController]
    [Route("api/artikal")]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService _artikalService;

        public ArtikalController(IArtikalService artikalService)
        {
            _artikalService = artikalService;
        }

        [HttpGet]
        public async Task<IActionResult> SviArtikli()
        {

            var artikli = await _artikalService.PreuzmiSveArtikle();
            return Ok(artikli);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ArtikalPoId(int id)
        {

            var artikal = await _artikalService.PreuzmiArtikalPoId(id);
            if (artikal != null)
            {
                return Ok(artikal);
            }
            else
            {
                return NotFound("Nije pronadjen artikal");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DodajArtikal([FromBody] Artikal artikal)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _artikalService.DodajNoviArtikal(artikal);
                return Ok("Uspjesno ste dodali artikal");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AzurirajArtikal(int id, [FromBody] Artikal artikal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artikal.IdArtikla)
            {
                return BadRequest("ID artikla se ne podudara sa ID-em u zahtevu.");
            }



            try
            {
                await _artikalService.AzurirajArtikal(artikal);
                return Ok("Artikal je uspjesno azuriran.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ObrisiArtikal(int id)
        {
            bool uspesnoObrisan = await _artikalService.ObrisiArtikal(id);
            if (!uspesnoObrisan)
            {
                return NotFound("Nije pronadjen artikal");
            }

            return Ok("Uspjesno obrisan artikal");
        }


    }
}
