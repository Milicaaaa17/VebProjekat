using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Controllers
{
    [ApiController]
    [Route("api/artikal")]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService _artikalService;
        private readonly IMapper _mapper;

        public ArtikalController(IArtikalService artikalService, IMapper mapper)
        {
            _artikalService = artikalService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Kupac")]
        public async Task<IActionResult> SviArtikli()
        {

            var artikli = await _artikalService.PreuzmiSveArtikle();
            return Ok(artikli);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Prodavac", Policy = "SamoVerifikovani")]
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
       // [Authorize(Roles = "Prodavac", Policy = "SamoVerifikovani")]
        [AllowAnonymous]
        public async Task<IActionResult> DodajArtikal([FromBody] KreirajArtikalDTO kreirajartikalDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
                try
                {
                    await _artikalService.DodajNoviArtikal(kreirajartikalDto);
                    return Ok("Uspješno ste dodali artikal");
                }
                catch (Exception ex)
                {
                    // Obrada specifičnih izuzetaka
                    if (ex.Message == "Artikal sa istim nazivom vec postoji")
                    {
                        return Conflict("Artikal sa istim nazivom već postoji");
                    }
                    else if (ex.Message == "Cijena artikla mora biti pozitivan broj")
                    {
                        return BadRequest("Cijena artikla mora biti pozitivan broj");
                    }
                    else
                    {
                        // Obrada ostalih izuzetaka
                        return StatusCode(StatusCodes.Status500InternalServerError, "Došlo je do greške prilikom dodavanja artikla.");
                    }

                }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Prodavac", Policy = "SamoVerifikovani")]
        public async Task<IActionResult> AzurirajArtikal(int id, [FromBody] ArtikalDTO artikalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artikalDto.IdArtikla)
            {
                return BadRequest("ID artikla se ne podudara sa ID-em u zahtevu.");
            }

            Artikal artikal = _mapper.Map<Artikal>(artikalDto);

            try
            {
                await _artikalService.AzurirajArtikal(artikalDto);
                return Ok("Artikal je uspješno ažuriran.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Prodavac", Policy = "SamoVerifikovani")]
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
