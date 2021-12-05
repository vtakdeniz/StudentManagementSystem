using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string lecture_name { get; set; }
        public string classroom_code { get; set; }
        public Teacher lecturer { get; set; }
        public List<Student_has_lectures> lecture_Has_Students { get; set; }
    }
}
