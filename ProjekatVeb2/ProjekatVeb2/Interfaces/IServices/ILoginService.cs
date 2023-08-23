using ProjekatVeb2.DTO;
using ProjekatVeb2.Models.Ispis;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface ILoginService
    {
        Task<Prijava> PrijavaKorisnika(LoginDTO loginDto);
        Task<Prijava> LoginGoogle(string t);


    }
}
