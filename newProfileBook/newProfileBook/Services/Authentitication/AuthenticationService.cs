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
            
            try
            {
                var profile = (IEnumerable<ProfileModel>)_repository.GetAllAsync<ProfileModel>();
                profile.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
                if (profile != null)
                    return 1;
                else
                    return 0;
            }
            catch (System.Exception e)
            {
                App.Current.MainPage.DisplayAlert("Exception",e.ToString(),null,"Ok");
            }
            return 0;
        }

    }
}
