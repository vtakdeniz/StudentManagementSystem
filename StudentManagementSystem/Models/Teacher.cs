using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int age { get; set; }
        public List<Lecture> lectures;
    }
}
