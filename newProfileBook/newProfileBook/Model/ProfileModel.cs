using newProfileBook.Model;
using SQLite;
using System;

namespace newProfileBook
{
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Confirm { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
