using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using XamarinForms.Database.Model;

namespace XamarinForms.Database
{
    public class TodoItemDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public TodoItemDatabase(string getLocalFilePath)
        {
            _database = new SQLiteAsyncConnection(getLocalFilePath);
            _database.CreateTableAsync<TodoItem>().Wait();
        }
        public Task<List<TodoItem>> GetItemsAsync()
        {
            return _database.Table<TodoItem>().ToListAsync();
        }
        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return _database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }
        public Task<TodoItem> GetItemAsync(int id)
        {
            return _database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return _database.DeleteAsync(item);
        }
    }
}