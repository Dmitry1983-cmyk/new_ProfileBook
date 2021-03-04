using newProfileBook.Services.Repository;
using newProfileBook.Services.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace newProfileBook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository _repository;
        private readonly ISettingsUsers _settingsUsers;

        public AuthorizationService(IRepository repository, ISettingsUsers settingsUsers)
        {
            _repository = repository;
            _settingsUsers = settingsUsers;
        }

        public int Authorizate(int id)
        {
            return _settingsUsers.CurrentUser = id;
        }

        public int RegisterAsync(ProfileModel profile)
        {
            if (profile.Id != 0)
            {
                _repository.UpdateAsync(profile);
                return profile.Id;
            }
            else
            {
                 _repository.InsertAsync(profile);
                return profile.Id;
            }
        }

    }
}
