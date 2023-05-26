namespace ProjekatVeb2.Interfaces
{
    public interface IEmailService
    {
        Task SendeEmail(string email, string verifikovanje);
    }
}
