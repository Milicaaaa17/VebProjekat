using AutoMapper;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Mapper
{
    public class MapDTO:Profile
    {
        public MapDTO() 
        {
            CreateMap<Korisnik, KorisnikDTO>().ReverseMap();
        }
    }
}
