
namespace newProfileBook.Services.Settings
{
    public interface ISettingsUsers
    {
        int CurrentUser { get; set; }
        int Sorting { get; set; }
        int Theme { get; set; }
        string Language { get; set; }
    }
}
