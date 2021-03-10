using newProfileBook.Model;
using newProfileBook.Services.Repository;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;


namespace newProfileBook.Services.Authentitication
{
    public class AuthenticationService : BindableBase, IAuthenticationService
    {
        private readonly IRepository<User> _repository;

        public AuthenticationService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public int Authenticate(string login, string password)
        {

            var user = _repository.GetItems().FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
                return user.Id;
            else
                return 0;
        }

    }
}
