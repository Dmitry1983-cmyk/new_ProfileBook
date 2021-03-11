
using newProfileBook.Model;

namespace newProfileBook.Services.Authentitication
{
     public interface IAuthenticationService
    {
        void RegistrationUser(string login, string password);
        Validator Validate(string login, string password, string confirmPassword);
    }
}
