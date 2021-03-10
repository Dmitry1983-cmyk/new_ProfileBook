using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace newProfileBook.Model
{
    public class User : BaseModel, INotifyPropertyChanged
    {
        private string login;
        private string password;
        private string confirm;

        public string Login
        {
            get { return login; }
            set
            {
                SetProperty(ref login, value);
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
        }
        public string Confirm
        {
            get { return confirm; }
            set
            {
                SetProperty(ref confirm, value);
            }
        }

    }
}
