using newProfileBook.Model;
using newProfileBook.Services.Repository;
using newProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newProfileBook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository<User> _repository;
        private readonly ISettingsUsers _settingsUsers;

        #region---ctor
        public AuthorizationService(IRepository<User> repository, ISettingsUsers settingsUsers)
        {
            _repository = repository;
            _settingsUsers = settingsUsers;
        }

        #endregion

        public int Authorizate(string login, string password)
        {
            var user = _repository.GetItems().Where(x => x.Login == login && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                _settingsUsers.CurrentUser = user.Id;
                return 1;
            }
            else return 0;
        }

    }
}
