using newProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace newProfileBook.Services
{
    public interface IProfileService
    {
        int SaveProfile(Profile profile);
        int DeleteProfile(int id);
        IEnumerable<Profile> GetProfiles();
        IEnumerable<Profile> SortProfiles();
    }
}
