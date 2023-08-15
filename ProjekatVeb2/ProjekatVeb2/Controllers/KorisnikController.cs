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
        private readonly IEnkripcijaService _enkripcijaService;
        private readonly IMapper _mapper;


        public KorisnikController(IKorisnikService korisnikService, IEnkripcijaService enkripcijaService, IMapper mapper)
        {
            _korisnikService = korisnikService;
            _enkripcijaService = enkripcijaService;
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
        [Authorize]
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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> AzuriranjeKorisnika(int id, [FromForm] IzmjenaProfilaDTO korisnikDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnikDto.Id)
            {
                return BadRequest("Id korisnika se ne poklapa sa Id vrijednoscu u rutiranju.");
            }

            try
            {
                await _korisnikService.AzurirajKorisnika(korisnikDto);
                return Ok("Korisnik je azuriran.");
            }
            catch (Exception ex)
            {

                return BadRequest($"Greska prilikom azuriranja: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}/status-verifikacije")]
        [Authorize]
        public async Task<IActionResult> DobaviStatusVerifikacije(int id)
        {
            var korisnik = await _korisnikService.KorisnikNaOsnovuId(id);
            if (korisnik != null)
            {
                return Ok(korisnik.StatusVerifikacije.ToString());
            }
            return NotFound("Nije pronadjen");
        }

    }
}
