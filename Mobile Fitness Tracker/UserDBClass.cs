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

        // [PrimaryKey, AutoIncrement]

        //Primary key Product_Number
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }       
        public string Exercise { get; set; }
        public string Description { get; set; }
        
        


    }



}
