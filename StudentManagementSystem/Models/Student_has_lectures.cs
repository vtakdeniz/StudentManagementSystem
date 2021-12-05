using System;
namespace StudentManagementSystem.Models
{
    public class Student_has_lectures
    {
        public int student_id{ get; set; }
        public Student student { get; set; }

        public int lecture_id { get; set; }
        public Lecture lecture { get; set; }

    }
}
