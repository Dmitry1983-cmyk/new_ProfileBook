using newProfileBook.Model;
using newProfileBook.Services.Repository;
using newProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace newProfileBook.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Profile> _repository;
        private readonly ISettingsUsers _settingsUsers;

        #region---ctor

        public ProfileService(IRepository<Profile> repository, ISettingsUsers settingsUsers)
        {
            _repository = repository;
            _settingsUsers = settingsUsers;
        }

        #endregion
        public int DeleteProfile(int id)
        {
            return _repository.DeleteItem(id);
        }

        public IEnumerable<Profile> GetProfiles()
        {
            return _repository.GetItems();
        }

        public int SaveProfile(Profile profile)
        {
            if (profile.Id != 0)
            {
                _repository.UpdateItem(profile);
                return profile.Id;
            }
            else
            {
                return _repository.InsertItem(profile);
            }
        }

        public IEnumerable<Profile> SortProfiles()
        {
            int sortKey = _settingsUsers.Sorting;
            switch (sortKey)
            {
                case 0:
                    return _repository.GetItems().Where(x => x.New_Id == _settingsUsers.CurrentUser).OrderBy(x => x.DateTime);
                case 1:
                    return _repository.GetItems().Where(x => x.New_Id == _settingsUsers.CurrentUser).OrderBy(x => x.Name);
                case 2:
                    return _repository.GetItems().Where(x => x.New_Id == _settingsUsers.CurrentUser).OrderBy(x => x.Nickname);
                default:
                    return _repository.GetItems().Where(x => x.New_Id == _settingsUsers.CurrentUser).OrderBy(x => x);
            }
        }
    }
}
