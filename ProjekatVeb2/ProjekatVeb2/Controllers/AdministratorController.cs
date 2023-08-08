using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.Interfaces.IServices;

namespace ProjekatVeb2.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _adminService;

        public AdministratorController(IAdministratorService adminService)
        {
            _adminService = adminService;
        }



        [HttpGet("sviprodavci")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SviProdavci()
        {
            var prodavci = await _adminService.SviProdavci();
            return Ok(prodavci);
        }

        [HttpGet("sviverifikovaniprodavci")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SviVerifikovaniProdavci()
        {
            var prodavci = await _adminService.SviVerifikovaniProdavci();
            return Ok(prodavci);
        }

        [HttpGet("sviodbijeniprodavci")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SviOdbijeniProdavci()
        {
            var prodavci = await _adminService.SviOdbijeniProdavci();
            return Ok(prodavci);
        }


        [HttpGet("sviprodavcikojicekajuverifikaciju")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SviProdavciKojiCekajuVerifikaciju()
        {
            var prodavci = await _adminService.SviProdavciKojiCekajuVerifikaciju();
            return Ok(prodavci);
        }

        [HttpPost("registracije/{id}/prihvati-verifikaciju")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PrihvatiRegistraciju(int id)
        {
            var korisnik = await _adminService.VerifikovanProdavac(id);
            var uspjesno = await _adminService.AdminVerifikujeProdavca(id);
            if (uspjesno)
            {
                return Ok("Verifikovan prodavac");
            }
            return NotFound("Nije pronadjen");
        }


        [HttpPost("registracije/{id}/odbij-verifikaciju")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> OdbijRegistracijuProdavca(int id)
        {
            var uspjesno = await _adminService.AdminOdbijaVerifikacijuProdavca(id);
            if (uspjesno)
            {
                return Ok("Verifikacija je odbijena");
            }
            return NotFound("Korisnik nije pronađen");
        }


        [HttpGet("sviKorisnici")]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> SviKorisnici()
        {
            var korisnici = await _adminService.SviKorisnici();
            return Ok(korisnici);
        }
    }
}
