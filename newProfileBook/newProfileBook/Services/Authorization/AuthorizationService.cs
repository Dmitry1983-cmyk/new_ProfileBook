using newProfileBook.Model;
using newProfileBook.Services.Repository;
using newProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
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

        public int Authorizate(int id)
        {
            return _settingsUsers.CurrentUser = id;
        }

        public int RegisterAsync(User profile)
        {
            if (profile.Id != 0)
            {
                _repository.UpdateItem(profile);
                return profile.Id;
            }
            else
            {
                 _repository.InsertItem(profile);
                return profile.Id;
            }
        }

    }
}
