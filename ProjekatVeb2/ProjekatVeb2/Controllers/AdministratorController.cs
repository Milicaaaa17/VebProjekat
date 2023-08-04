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
        public async Task<IActionResult> PrikaziRegistracije()
        {
            var korisnici = await _adminService.DohvatiRegistracijeZaOdobrenje();
            return Ok(korisnici);
        }

        [HttpPost("registracije/{id}/odobri")]
        public async Task<IActionResult> OdobriRegistraciju(int id)
        {
            var uspjesno = await _adminService.AdminOdobravaRegistraciju(id);
            if (uspjesno)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("registracije/{id}/odbij")]
        public async Task<IActionResult> OdbijRegistraciju(int id)
        {
            await _adminService.AdminOdbijaRegistraciju(id);
            return Ok();
        }
    }
}
