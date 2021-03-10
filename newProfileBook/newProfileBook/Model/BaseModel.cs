using newProfileBook.Services.Settings;
using Prism.Mvvm;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace newProfileBook.Model
{
    public class BaseModel : BindableBase
    {
        protected ISettingsUsers SettingsUser { get; private set; }
        private int id;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set
            {
                SetProperty(ref id, value);
            }
        }

    }
}
