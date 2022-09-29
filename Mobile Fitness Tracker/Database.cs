using SQLite;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;

namespace Mobile_Fitness_Tracker
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserDBClass>();//user table
            _database.CreateTableAsync<ExerciseDBClass>();//exercise table
        }

        //---------------USER PROFILE DB------------------//
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
            await _database.DeleteAllAsync<UserDBClass>();
        }


        //-------------Exerice DB-----------------------------//

        public Task<List<ExerciseDBClass>> GetExerciseAsync()
        {
            return _database.Table<ExerciseDBClass>().ToListAsync();
           

        }

        public Task<int> SaveExerciseAsync(ExerciseDBClass exercise)
        {
            return _database.InsertAsync(exercise);

        }

        internal async Task DeleteAllExrcises()
        {
           //delete table content
            await _database.DeleteAllAsync<ExerciseDBClass>();
           //delete increment ID
            await _database.ExecuteAsync("delete from sqlite_sequence where name = 'ExerciseDBClass';");           

        }

        internal async Task DeleteRow()
        {
            //delete table content
            // await _database.DeleteAllAsync<ExerciseDBClass>();
            //delete increment ID
            int digit = UserGlobalVaraibles.selection;
             await _database.ExecuteAsync($"delete from ExerciseDBClass where Id = {digit} ;");

           // return  _database.QueryAsync<int>("SELECT * FROM ExerciseDBClass").ToListAsync();
            //await _database.ExecuteAsync("delete from sqlite_sequence where name = 'ExerciseDBClass';");
        }




    }


}
      




    

