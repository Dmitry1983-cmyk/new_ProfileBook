using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace newProfileBook.ViewModel
{
   class ProfileListVewModel : INotifyPropertyChanged
    {
        public ProfileModel Profile { get; set; }
        public MainListPageViewModel ListViewModel { get; set; }


        public ProfileListVewModel()
        {
            Profile = new ProfileModel();
        }


        public string Name
        {
            get { return Profile.Name; }
            set
            {
                if (Profile.Name!=null)
                {
                    Profile.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Nickname
        {
            get { return Profile.Nickname; }
            set
            {
                if (Profile.Nickname != null)
                {
                    Profile.Nickname = value;
                    OnPropertyChanged("Nickname");
                }
            }
        }

        public string Login
        {
            get { return Profile.Login; }
            set
            {
                if (Profile.Login != null)
                {
                    Profile.Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return Profile.Password; }
            set
            {
                if (Profile.Password != null)
                {
                    Profile.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Confirm
        {
            get { return Profile.Confirm; }
            set
            {
                if (Profile.Confirm != null)
                {
                    Profile.Confirm = value;
                    OnPropertyChanged("Confirm");
                }
            }
        }

        public string ImagePath
        {
            get { return Profile.ImagePath; }
            set
            {
                if (Profile.ImagePath != null)
                {
                    Profile.ImagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }

        public string Description
        {
            get { return Profile.Description; }
            set
            {
                if (Profile.Description != null)
                {
                    Profile.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
