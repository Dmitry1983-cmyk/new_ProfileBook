using newProfileBook.Model;
using newProfileBook.Services.Repository;
using System.Linq;
using System.Text.RegularExpressions;

namespace newProfileBook.Services.Authentitication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _repository;
        public AuthenticationService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public void RegistrationUser(string login, string password)
        {
            User user = new User
            {
                Login = login,
                Password = password
            };

            _repository.InsertItem(user);
        }

        public bool Authenticate(string login)
        {
            User user = _repository.GetItems().Where(x => x.Login == login).FirstOrDefault();
            return user != null;
        }

        private bool RegexFunc(string password)
        {
            var num = new Regex(@"[0-9]+");
            var upperCase = new Regex(@"[A-Z]+");
            var lowerCase = new Regex(@"[a-z]+");

            return num.IsMatch(password) && upperCase.IsMatch(password) && lowerCase.IsMatch(password);
        }

        public Validator Validate(string login, string password, string confirmPassword)
        {
            Validator valid;
            if (Authenticate(login)) { valid = Validator.LoginIsTaken; }
            else if (login.Length > 16) { valid = Validator.LoginIsTooLong; }
            else if (login.Length==0 || login.Length < 4) { valid = Validator.LoginIsTooShort; }
            else if (char.IsDigit(login[0])) { valid = Validator.LoginStartsWithNumber; }
            else if (password.Length > 16) { valid = Validator.PasswordIsTooLong; }
            else if (password.Length==0 || password.Length < 8) { valid = Validator.PasswordIsTooShort; }
            else if (!RegexFunc(password)) { valid = Validator.PasswordIsWeak; }
            else if (!password.Equals(confirmPassword)) { valid = Validator.PasswordsAreNotEqual; }
            else { valid = Validator.Success; }

            return valid;
        }

    }
}
