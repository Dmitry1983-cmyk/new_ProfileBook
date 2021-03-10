using newProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace newProfileBook.Services.Repository
{
    public  interface IRepository<T> where T : BaseModel, new()
    {
        IEnumerable<T> GetItems();
        T GetItem(int id);
        int DeleteItem(int id);
        int DeleteAllItems();
        int UpdateItem(T item);
        int InsertItem(T item);
    }
}
