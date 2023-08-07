using ProjekatVeb2.Models;
namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IEmailService
    {
        void PosaljiEmail(Message message);
    }
}
