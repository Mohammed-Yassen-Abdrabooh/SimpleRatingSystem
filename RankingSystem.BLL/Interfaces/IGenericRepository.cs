using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingSystem.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // signature for fuction to get all items
        Task<IEnumerable<T>> GetAllAsync();
        // signature for fuction to get item by id
        Task<T> GetByIdAsync(int? id);
        // signature for fuction to Add item
        int AddAsync(T item);
        // signature for fuction to Update item
        int UpdateAsync(T item);
        // signature for fuction to Delete item
        int DeleteAsync(T item);

      
    }
}
