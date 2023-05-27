using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces;

namespace ProjekatVeb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly IServiceAutentifikacija _serviceA;

        public AutentifikacijaController(IServiceAutentifikacija serviceA)
        {
            _serviceA = serviceA;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDTO loginDto)
        {
            try
            {
                string token = await _serviceA.Login(loginDto);

                if (token == null)
                    return BadRequest();

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
