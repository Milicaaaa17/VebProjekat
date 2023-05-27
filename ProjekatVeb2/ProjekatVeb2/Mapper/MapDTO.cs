using AutoMapper;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;
using System.Text;

namespace ProjekatVeb2.Mapper
{
    public class MapDTO:Profile
    {
        public MapDTO() 
        {
            CreateMap<Korisnik, KorisnikDTO>().ReverseMap();
            
            CreateMap<RegistracijaDTO, Korisnik>()
                .ForMember(dest => dest.Slika, opt => opt.MapFrom(src => Encoding.ASCII.GetBytes(src.Slika)));



        }
    }
}
