
namespace newProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        int Authorizate(int id);
        int RegisterAsync(ProfileModel item);
    }
}
