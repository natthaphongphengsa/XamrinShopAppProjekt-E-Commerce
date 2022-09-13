using Shopping_App.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping_App.Data
{
    public class MyDatabase
    {
        public SQLiteAsyncConnection database;

        public MyDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
            database.CreateTableAsync<User>().Wait();
        }
        #region Products
        public Task<List<Item>> GetItemsAsync()
        {
            //Get all notes.
            return database.Table<Item>().ToListAsync();
        }
        public Task<Item> GetItemAsync(int id)
        {
            // Get a specific note.
            return database.Table<Item>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(Item item)
        {
            //var i = GetItemAsync(item.Id);
            if (item.Id != 0)
            {
                // Update an existing item.
                return database.UpdateAsync(item);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(Item item)
        {
            // Delete an item.
            return database.DeleteAsync(item);
        }
        #endregion

        #region User

        public Task<List<User>> GetUsersAsync()
        {
            return database.Table<User>().ToListAsync();
        }
        public Task<User> GetUserAsync(int id)
        {
            return database.Table<User>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(user);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(user);
            }
        }
        public Task<int> DeleteUserAsync(User user)
        {
            return database.DeleteAsync(user);
        }
        #endregion;
    }
}