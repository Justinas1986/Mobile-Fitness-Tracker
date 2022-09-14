using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Fitness_Tracker
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserDBClass>();
        }

        public Task<List<UserDBClass>> GetPeopleAsync()
        {
            return _database.Table<UserDBClass>().ToListAsync();
        }

        public Task<int> SavePersonAsync(UserDBClass person)
        {
            return _database.InsertAsync(person);
        }
        internal async Task DeleteAll()
        {
            await _database.DeleteAllAsync< UserDBClass>();
        }

    }
}
