using newProfileBook.Services.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace newProfileBook.Services.Authentitication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository _repository;
        public AuthenticationService(IRepository repository)
        {
            _repository = repository;
        }
        public int Authenticate(string login, string password)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profilebook_2.db3");
            var database = new SQLiteAsyncConnection(path);

            var user = database.Table<ProfileModel>().Where(x => x.Login == login && x.Password == password).FirstOrDefaultAsync();
            if (user != null)
                return user.Id;
            else
                return 0;
        }
    }
}
