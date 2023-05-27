using ProjekatVeb2.DTO;

namespace ProjekatVeb2.Interfaces
{
    public interface IServiceAutentifikacija
    {
        Task<string> Login(LoginDTO loginDto);
    }
}
