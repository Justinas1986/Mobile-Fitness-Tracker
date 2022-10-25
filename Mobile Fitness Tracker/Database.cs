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
        //create sql connection
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserDBClass>();//user table
            _database.CreateTableAsync<ExerciseDBClass>();//exercise table
            _database.CreateTableAsync<WorkoutDBClass>();//workout table
            _database.CreateTableAsync<WorkoutExerciseDBClass>();//workout+exercise table
            _database.CreateTableAsync<WorkoutScheduleDBClass>();//workoutschedule table
        }

        //---------------USER PROFILE DB------------------//
       //get user profile info from db
        public Task<List<UserDBClass>> GetPeopleAsync()
        {
            return _database.Table<UserDBClass>().ToListAsync();

        }
        //save user profile info to db 
        public Task<int> SavePersonAsync(UserDBClass person)
        {
            return _database.InsertAsync(person);

        }

        //delelte all user info from db
        internal async Task DeleteAll()
        {
            await _database.DeleteAllAsync<UserDBClass>();
        }


        //-------------Exerice DB-----------------------------//
        //get exercise info from db
        public Task<List<ExerciseDBClass>> GetExerciseAsync()
        {
            return _database.Table<ExerciseDBClass>().ToListAsync();
           

        }
        //save exercise info to db 
        public Task<int> SaveExerciseAsync(ExerciseDBClass exercise)
        {
            return _database.InsertAsync(exercise);

        }
        //delete all exercises
        internal async Task DeleteAllExrcises()
        {
           //delete table content
            await _database.DeleteAllAsync<ExerciseDBClass>();
           //delete increment ID
            await _database.ExecuteAsync("delete from sqlite_sequence where name = 'ExerciseDBClass';");           

        }
        //delete selected row
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
        //get workouts from db
        public Task<List<WorkoutDBClass>> GetWorkoutListAsync()
        {           
           return _database.QueryAsync<WorkoutDBClass>("SELECT Workout FROM WorkoutDBClass");
        }

        //-------------Workout+Exercise DB-----------------------------//
        //Full tabale list Workout+Exercise
        public Task<List<WorkoutExerciseDBClass>> GetWorkoutExerciseAsync()
        {
            return _database.Table<WorkoutExerciseDBClass>().ToListAsync();

        }
        //Save all exercise and workouts to list
        public Task<int> SaveWorkoutExerciseAsync(WorkoutExerciseDBClass workout)
        {
            return _database.InsertAsync(workout);

        }
        //Get exercise Workout+Exercise associated with selected workout
        public Task<List<WorkoutExerciseDBClass>> WorkoutExerciseAsync()
          {
             string workout = UserGlobalVaraibles.workoutcellValue;
            //Query to get exercises related to selected workout
            return _database.QueryAsync<WorkoutExerciseDBClass>($"SELECT * FROM WorkoutExerciseDBClass WHERE Workout = '{workout}'");
          }
        //delete row when deleted from Workout+Exercise DB using Workout Id   
        internal async Task DeleteWorkoutExerciseRow()
        {            
           int digit = UserGlobalVaraibles.workoutexercise_Id;
            //query delete selected row from Workout+Exercise list by Workout Id
            await _database.ExecuteAsync($"delete from WorkoutExerciseDBClass where Id = {digit} ;");
           
        }
        //delete row when deleted from Workout+Exercise list using Exercise Id   
        internal async Task DeleteWorkoutExerciseRow2()
        {
            int digit = UserGlobalVaraibles.exerciseIdValue;
            //query delete selected row by Exercise Id          
            await _database.ExecuteAsync($"delete from WorkoutExerciseDBClass where ExerciseId = {digit} ;");
        }

        //delete all
        internal async Task DeleteAllWorkoutExercise()
        {
            //delete table content
            //await _database.DeleteAllAsync<WorkoutExerciseDBClass>();
            //delete increment ID
            string workout = UserGlobalVaraibles.workoutcellValue;
            await _database.ExecuteAsync($"delete from WorkoutExerciseDBClass WHERE Workout = '{workout}';");

        }

        

        //-------------Workout Schedule DB-----------------------------//

        //get all workout values from database
        public Task<List<WorkoutScheduleDBClass>> GetWorkoutScheduleAsync()
        {
            return _database.Table<WorkoutScheduleDBClass>().ToListAsync();

        }
        //save to workout to database
        public Task<int> SaveWorkoutScheduleAsync(WorkoutScheduleDBClass workout)
        {
            return _database.InsertAsync(workout);

        }

        //get workout name from grid view
        public Task<List<WorkoutScheduleDBClass>> WorkoutScheduleAsync()
        {
            string workout = UserGlobalVaraibles.workoutcellValue;
            return _database.QueryAsync<WorkoutScheduleDBClass>($"SELECT Workout FROM WorkoutDBClass WHERE Workout = '{workout}'");
        }

        //delete workout row from data grid view
        internal async Task DeleteWorkoutScheduleRow()
        {
            int digit = UserGlobalVaraibles.cellValue;
            //query delete selected row by Id
            await _database.ExecuteAsync($"delete from WorkoutScheduleDBClass where Id = {digit} ;");
        }

        //update workout with date and time query
        public Task<List<WorkoutScheduleDBClass>> WorkoutScheduleUpdateAsync()
        {
            string workout = UserGlobalVaraibles.workoutcellValue;
            //update workout with date and time
            return _database.QueryAsync<WorkoutScheduleDBClass>($"UPDATE WorkoutDBClass SET Date = '{UserGlobalVaraibles.Date}', Time = '{UserGlobalVaraibles.Time}' WHERE Workout = '{workout}' ");
        }

       
    }


}
      




    

