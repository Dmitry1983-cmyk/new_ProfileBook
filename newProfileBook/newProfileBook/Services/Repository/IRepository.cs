using newProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace newProfileBook.Services.Repository
{
    public  interface IRepository
    {
        Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new();

        Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new();

        Task<int> DleteAsync<T>(T entity) where T : IEntityBase, new();

        Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new();

        Task<int> DeleteAllItems<T>(T entity) where T : IEntityBase, new();
    }
}
