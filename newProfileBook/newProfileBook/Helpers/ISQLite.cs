using SQLite;

namespace newProfileBook.Helpers
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
