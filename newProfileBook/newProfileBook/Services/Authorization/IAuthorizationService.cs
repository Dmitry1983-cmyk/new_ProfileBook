
using newProfileBook.Model;

namespace newProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        int Authorizate(string login, string password);
    }
}
