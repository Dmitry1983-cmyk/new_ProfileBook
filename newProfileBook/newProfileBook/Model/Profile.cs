using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace newProfileBook.Model
{
    public class Profile : BaseModel, INotifyPropertyChanged
    {
        private int _id;
        private string imgPath;
        private string nickname;
        private string name;
        private string description;
        private DateTime dateTime;


        public int New_Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }


        public string ImgPath
        {
            get { return imgPath; }
            set
            {
                SetProperty(ref imgPath, value);
            }
        }
        public string Nickname
        {
            get { return nickname; }
            set
            {
                SetProperty(ref nickname, value);
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                SetProperty(ref description, value);
            }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                SetProperty(ref dateTime, value);
            }
        }
    }
}
