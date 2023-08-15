using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;
using ProjekatVeb2.Models.Ispis;

namespace ProjekatVeb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegistracijaController:ControllerBase
    {
        private readonly IRegistracijaService _registracijaService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RegistracijaController(IRegistracijaService registracijaService, IConfiguration configuration, IMapper mapper)
        {
            _registracijaService = registracijaService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [AllowAnonymous]

        public async Task<IActionResult> Registracija([FromForm] RegistracijaDTO registracijaDTO)
        {
            var korisnik = _mapper.Map<Korisnik>(registracijaDTO);

            if (registracijaDTO.Tip == TipKorisnika.Kupac || registracijaDTO.Tip == TipKorisnika.Administrator)
            {
                korisnik.StatusVerifikacije = StatusVerifikacije.Odobren;
            }
            else
            {
                korisnik.StatusVerifikacije = StatusVerifikacije.UObradi;
            }

            Registrovanje rezultat = await _registracijaService.RegistrujKorisnika(registracijaDTO);
            if (rezultat.KorisnikRegistrovan)
            {
                return Ok(rezultat);
            }
            else
            {
                return BadRequest(new { errors = rezultat.Poruka });
            }
        }
    }
}
