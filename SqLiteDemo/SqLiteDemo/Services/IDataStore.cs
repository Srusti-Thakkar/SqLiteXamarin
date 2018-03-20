using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqLiteDemo.Services
{
    public interface IDataStore<T>
    {
        Task<int> SaveItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
