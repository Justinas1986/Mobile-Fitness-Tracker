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
            _database.CreateTableAsync<WorkoutDBClass>();//workout table
            _database.CreateTableAsync<WorkoutExerciseDBClass>();//workout+exercise table
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
            int digit = UserGlobalVaraibles.cellValue;
            //query delete selected row by Id
             await _database.ExecuteAsync($"delete from ExerciseDBClass where Id = {digit} ;");
        }

        //-------------Workout DB-----------------------------//

        //get all workout values from database
        public Task<List<WorkoutDBClass>> GetWorkoutAsync()
        {
            return _database.Table<WorkoutDBClass>().ToListAsync();
          
        }
        //save to workout to database
        public Task<int> SaveWorkoutAsync(WorkoutDBClass workout)
        {
            return _database.InsertAsync(workout);

        }

        //get workout name from grid view
        public Task<List<WorkoutDBClass>> WorkoutAsync()
        {
            string workout = UserGlobalVaraibles.workoutcellValue;
            return _database.QueryAsync<WorkoutDBClass>($"SELECT Workout FROM WorkoutDBClass WHERE Workout = '{workout}'");
        }

        //delete workout row from data grid view
        internal async Task DeleteWorkoutRow()
        {
            int digit = UserGlobalVaraibles.cellValue;
            //query delete selected row by Id
            await _database.ExecuteAsync($"delete from WorkoutDBClass where Id = {digit} ;");
        }

        //update workout with date and time query
        public Task<List<WorkoutDBClass>> WorkoutUpdateAsync()
        {
            string workout = UserGlobalVaraibles.workoutcellValue;
            //update workout with date and time
            return _database.QueryAsync<WorkoutDBClass>($"UPDATE WorkoutDBClass SET Date = '{UserGlobalVaraibles.Date}', Time = '{UserGlobalVaraibles.Time}' WHERE Workout = '{workout}' ");
        }

        //-------------Workout+Exercise DB-----------------------------//
        //Full tabale list
        public Task<List<WorkoutExerciseDBClass>> GetWorkoutExerciseAsync()
        {
            return _database.Table<WorkoutExerciseDBClass>().ToListAsync();

        }
        //Save all exercise and workouts to list
        public Task<int> SaveWorkoutExerciseAsync(WorkoutExerciseDBClass workout)
        {
            return _database.InsertAsync(workout);

        }
        //Get exercise list associated with selected workout
          public Task<List<WorkoutExerciseDBClass>> WorkoutExerciseAsync()
          {
             string workout = UserGlobalVaraibles.workoutcellValue;
            //Query to get exercises related to selected workout
            return _database.QueryAsync<WorkoutExerciseDBClass>($"SELECT * FROM WorkoutExerciseDBClass WHERE Workout = '{workout}'");
          }
        //delete row 
        internal async Task DeleteWorkoutExerciseRow()
        {            
           int digit = UserGlobalVaraibles.workoutexercise_Id;           
            //query delete selected row by Id
            await _database.ExecuteAsync($"delete from WorkoutExerciseDBClass where Id = {digit} ;");
        }
        //delete all
        internal async Task DeleteAllWorkoutExercise()
        {
            //delete table content
            await _database.DeleteAllAsync<WorkoutExerciseDBClass>();
            //delete increment ID
            await _database.ExecuteAsync("delete from sqlite_sequence where name = 'WorkoutExerciseDBClass';");

        }

    }


}
      




    

