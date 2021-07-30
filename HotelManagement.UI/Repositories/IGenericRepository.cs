using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.UI.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync();
        void Add(T model);
        void Remove(T model);
        void AddOrUpdate(T model);
    }
}
