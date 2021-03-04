using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings.Abstractions;

namespace newProfileBook.Services.Setting
{
    class SettingsUsers : ISettingsUsers
    {
        private readonly ISettings _settings;

        public SettingsUsers(ISettings settings)
        {
            _settings = settings;
        }

        public int CurrentUser
        {
            get => _settings.GetValueOrDefault(nameof(CurrentUser), -1);
            set => _settings.AddOrUpdateValue(nameof(CurrentUser), value);
        }
    }
}
