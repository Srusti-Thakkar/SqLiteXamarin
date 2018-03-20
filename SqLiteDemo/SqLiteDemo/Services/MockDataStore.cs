using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using SqLiteDemo.Models;

[assembly: Xamarin.Forms.Dependency(typeof(SqLiteDemo.Services.MockDataStore))]
namespace SqLiteDemo.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        SQLiteAsyncConnection connection;
        List<Item> items;

        public MockDataStore()
        {
            connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDb.db3"));
            connection.CreateTableAsync<Item>().Wait();
        }

        public async Task<int> SaveItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                return await connection.UpdateAsync(item);
            }
            else
            {
                return await connection.InsertAsync(item);
            }
            
        }        

        public async Task<int> DeleteItemAsync(Item item)
        {
            return await connection.DeleteAsync(item);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await connection.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await connection.Table<Item>().ToListAsync();
        }
    }
}