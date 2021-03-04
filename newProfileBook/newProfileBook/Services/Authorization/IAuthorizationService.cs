using newProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace newProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        int Authorizate(int id);
        int RegisterAsync(ProfileModel item);
    }
}
