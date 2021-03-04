using System;
using System.Collections.Generic;
using System.Text;

namespace newProfileBook.Services.Authentitication
{
     public interface IAuthenticationService
    {
        int Authenticate(string login, string password);
    }
}
