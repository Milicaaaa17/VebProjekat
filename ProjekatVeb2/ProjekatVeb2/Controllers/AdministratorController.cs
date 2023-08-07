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

        [HttpGet("registracije")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PrikaziRegistracije()
        {
            var korisnici = await _adminService.DohvatiRegistracijeZaOdobrenje();
            return Ok(korisnici);
        }


        [HttpPost("registracije/{id}/odobri")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> OdobriRegistraciju(int id)
        {
            var uspjesno = await _adminService.AdminOdobravaRegistraciju(id);
            if (uspjesno)
            {
               
                return Ok("Registracija uspjesno odobrena");
            }
           
            return NotFound("Nije pronadjena");
        }

        [HttpPost("registracije/{id}/odbij")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> OdbijRegistraciju(int id)
        {
            
            var uspjesno = await _adminService.AdminOdbijaRegistraciju(id);
            if (uspjesno)
            {
                return Ok("Registracija odbijena");
            }
            return NotFound("Nije pronadjena");
        }
    }
}
