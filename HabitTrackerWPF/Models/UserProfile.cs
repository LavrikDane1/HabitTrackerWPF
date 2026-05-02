using System;
using System.Collections.Generic;

namespace HabitTracker.Models
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Education { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<string> Hobbies { get; set; } = new List<string>();
        public string ActivityLevel { get; set; }
    }
}
