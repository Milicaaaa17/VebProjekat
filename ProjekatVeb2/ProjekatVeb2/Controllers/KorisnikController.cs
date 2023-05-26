using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces;
using System.Data;

namespace ProjekatVeb2.Controllers
{
    public class KorisnikController : ControllerBase
    {
        private readonly IServiceKorisnik korisnikServis;

        public KorisnikController(IServiceKorisnik kServis)
        {
            korisnikServis = kServis;
        }

        [HttpGet("GetSviKorisnici")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetSvi_Korisnici()
        {
            try
            {
                List<KorisnikDTO> korisnici = await korisnikServis.GetSviKorisnici();

                if (korisnici.Count == 0)
                {
                    return NotFound();
                }

                return Ok(korisnici);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interna server greška: {ex.Message}");
            }
        }

        [HttpGet("GetSviProdavci")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetSvi_Prodavci()
        {
            try
            {
                List<KorisnikDTO> prodavci = await korisnikServis.GetSviProdavci();

                if (prodavci.Any())
                {
                    return Ok(prodavci);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interna server greška: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromQuery] int id, [FromBody] UpdateKorisnikaDTO DtoK)
        {
            try
            {
                KorisnikDTO korisnik = await korisnikServis.UpdateKorisnik(id, DtoK);

                if (korisnik != null)
                {
                    return Ok(korisnik);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interna server greška: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistracijaDTO DtoReg)
        {
            try
            {
                KorisnikDTO korisnik = await korisnikServis.Registracija(DtoReg);

                if (korisnik != null)
                {
                    return Ok(korisnik);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interna server greška: {ex.Message}");
            }
        }
    }

}
