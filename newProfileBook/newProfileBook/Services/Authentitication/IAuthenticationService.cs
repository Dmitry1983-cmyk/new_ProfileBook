
namespace newProfileBook.Services.Authentitication
{
     public interface IAuthenticationService
    {
        int Authenticate(string login, string password);
    }
}
