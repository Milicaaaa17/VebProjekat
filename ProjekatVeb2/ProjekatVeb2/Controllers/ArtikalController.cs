using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService _artikalService;
        private readonly IMapper _mapper;

        public ArtikalController(IArtikalService artikalService, IMapper mapper)
        {
            _artikalService = artikalService;
            _mapper = mapper;
        }

        [HttpGet("sviArtikli")]
        [Authorize(Roles = "Kupac")]
        public async Task<IActionResult> SviArtikli()
        {

            var artikli = await _artikalService.PreuzmiSveArtikle();
            return Ok(artikli);
        }

        [HttpGet("sviArtikliProdavac/{prodavacId}")]
        [Authorize(Roles = "Prodavac", Policy = "SamoVerifikovani")]
        public async Task<IActionResult> GetArtikliProdavca(int prodavacId)
        {
            var artikli = await _artikalService.DohvatiArtikleProdavca(prodavacId);
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
        [Authorize(Roles = "Prodavac", Policy = "SamoVerifikovani")]
        [AllowAnonymous]
        public async Task<IActionResult> DodajArtikal([FromForm] KreirajArtikalDTO kreirajartikalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _artikalService.DodajNoviArtikal(kreirajartikalDto);
                return Ok("Uspjesno ste dodali artikal");
            }
            catch (Exception ex)
            {

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

                    return StatusCode(StatusCodes.Status500InternalServerError, "Došlo je do greške prilikom dodavanja artikla.");
                }
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Prodavac", Policy = "SamoVerifikovani")]

        public async Task<IActionResult> AzuriranjeArtikla(int id, [FromForm] IzmijeniArtikalDTO artikalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artikalDto.IdArtikla)
            {
                return BadRequest("Id artikla se ne poklapa sa Id vrijednoscu u rutiranju.");
            }

            try
            {
                await _artikalService.AzurirajArtikal(artikalDto);
                return Ok("Artikal je azuriran.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Greska prilikom azuriranja: {ex.InnerException?.Message}");
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


        [HttpGet("detaljiPorudzbine/{porudzbinaId}")]
        [Authorize]
        public async Task<IActionResult> DetaljiPorudzbine(int porudzbinaId)
        {
            var artikli = await _artikalService.DobaviArtiklePorudzbine(porudzbinaId);
            return Ok(artikli);
        }

    }
}
