using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IServices;

namespace ProjekatVeb2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikService _korisnikService;
        private readonly IMapper _mapper;

        public KorisnikController(IKorisnikService korisnikService, IMapper mapper)
        {
            _korisnikService = korisnikService;        
            _mapper = mapper;
        }

        [HttpDelete("{id}")]

        [AllowAnonymous]
        public async Task<IActionResult> BrisanjeKorisnika(int id)
        {

            var uspjesno = await _korisnikService.BrisanjeKorisnikaNaOsnovuId(id);
            if (uspjesno)
            {
                return Ok("Uspjesno obrisan korisnik");
            }
            return NotFound("Nije pronadjen");

        }

        [HttpGet("{id}")]

        [AllowAnonymous]
        public async Task<IActionResult> PronadjiKorisnikaSaId(int id)
        {
            var korisnik = await _korisnikService.KorisnikNaOsnovuId(id);
            if (korisnik != null)
            {
                return Ok(korisnik);
            }
            else
            {
                return NotFound("Nije pronadjen");
            }

        }


       
        [HttpGet("{id}/profil")]
        [AllowAnonymous]
        public async Task<IActionResult> DobaviProfilKorisnika(int id)
        {
            var korisnik = await _korisnikService.KorisnikNaOsnovuId(id);
            if (korisnik != null)
            {

                var korisnikDto = _mapper.Map<KorisnikDTO>(korisnik);
                return Ok(korisnikDto);
            }
            return NotFound("Nije pronadjen");
        }

    }
}
