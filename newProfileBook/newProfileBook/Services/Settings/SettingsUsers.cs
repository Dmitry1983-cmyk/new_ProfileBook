using Plugin.Settings.Abstractions;
using Xamarin.Forms;
using newProfileBook.Model;
using newProfileBook.Localization;

namespace newProfileBook.Services.Settings
{
    public class SettingsUsers : ISettingsUsers
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

        public int Sorting
        {
            get => _settings.GetValueOrDefault(nameof(Sorting), (int)Sorted.DateTime);
            set => _settings.AddOrUpdateValue(nameof(Sorting), value);
        }
        public int Theme
        {
            get => _settings.GetValueOrDefault(nameof(Theme), (int)OSAppTheme.Light);
            set
            {
                _settings.AddOrUpdateValue(nameof(Theme), value);
                Application.Current.UserAppTheme = (OSAppTheme)value;
            }
        }
        public string Language
        {
            get => _settings.GetValueOrDefault(nameof(Language), "en");
            set
            {
                _settings.AddOrUpdateValue(nameof(Language), value);
                MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(value));
            }
        }

    }
}
