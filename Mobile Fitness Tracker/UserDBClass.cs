using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Fitness_Tracker
{
    public class UserDBClass
    {       
            [PrimaryKey]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PrefferedName { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Age { get; set; }
        public string ProfilePic { get; set; }
    }
}
