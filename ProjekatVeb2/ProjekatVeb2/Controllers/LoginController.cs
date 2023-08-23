using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IServices;

namespace ProjekatVeb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rezultat = await _loginService.PrijavaKorisnika(loginDto);

            if (rezultat.UspjesnaPrijava)
            {

                return Ok(new { Token = rezultat.Token });
            }
            else
            {
                return BadRequest(rezultat);
            }
        }
        [HttpPost("socialLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromForm] string googleToken)
        {
            try
            {
                var rezultatPrijave = await _loginService.LoginGoogle(googleToken);

                return Ok(rezultatPrijave.Token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
