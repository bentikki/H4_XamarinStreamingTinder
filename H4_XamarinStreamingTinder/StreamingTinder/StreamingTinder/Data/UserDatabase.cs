using SQLite;
using StreamingTinder.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinder.Data
{
    public class UserDatabase
    {
        readonly SQLiteAsyncConnection database;

        public UserDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UserLocal>().Wait();
        }

        public Task<UserLocal> GetCurrentLocalUserAsync()
        {
            // Get a specific note.
            return database.Table<UserLocal>().FirstOrDefaultAsync();
        }


        public Task<int> SaveLocalUserDataAsync(UserLocal user)
        {
            if (user.ID != 0)
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

        public Task<int> EmptyLocalUserRecordsAsync()
        {
            // Delete a note.
            return database.DeleteAllAsync<UserLocal>();
        }
    }
}
