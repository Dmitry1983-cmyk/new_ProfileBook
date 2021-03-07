using newProfileBook.Services.Repository;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;


namespace newProfileBook.Services.Authentitication
{
    public class AuthenticationService : BindableBase, IAuthenticationService
    {
        private readonly IRepository _repository;

        public AuthenticationService(IRepository repository)
        {
            _repository = repository;
        }

        public int Authenticate(string login, string password)
        {
            var profile = (IEnumerable<ProfileModel>)_repository.GetAllAsync<ProfileModel>();//exception null ref
            profile.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (profile != null)
                return 1;
            else
                return 0;
        }

    }
}
