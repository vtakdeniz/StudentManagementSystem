using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int class_year { get; set; }
        public DateTime enrollment_date { get; set; } = DateTime.Now;
        public int age { get; set; }
        public List<Student_has_lectures> student_Has_Lectures{ get; set; }

    }
}
