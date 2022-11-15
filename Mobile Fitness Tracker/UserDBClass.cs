using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Fitness_Tracker
{
    //create DB table UserDBClass
   
    public class UserDBClass
    {       
            [PrimaryKey]
            public int Id { get; set; }
            public  string FirstName { get; set; }
            public string LastName { get; set; }
            public string PrefferedName { get; set; }
            public double Weight { get; set; }
            public double Height { get; set; }
            public int Age { get; set; }
            public double BMI { get; set; }
            public string ProfilePic { get; set; }
    }


    public class ExerciseDBClass
    {        
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Exercise { get; set; }
        public string Description { get; set; }

    }

    public class WorkoutDBClass
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Workout{ get; set; }
        //public string Date { get; set; }
        //public string Time { get; set; }

    }

    public class WorkoutExerciseDBClass
    {
        [PrimaryKey]
        [AutoIncrement]      
        public int Id { get; set; }       
        public string Workout { get; set; }
        public int ExerciseId { get; set; }
        public string Exercise { get; set; }
        public string Description { get; set; }
        public bool ck { get; set; }

    }

    public class WorkoutScheduleDBClass
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Workout { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

    }



}
