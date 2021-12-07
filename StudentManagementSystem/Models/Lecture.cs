using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Lecture Name")]
        public string lecture_name { get; set; }
        [Required]
        [DisplayName("Classroom Code")]
        public string classroom_code { get; set; }
        [Required]
        [DisplayName("Teacher Name")]
        public Teacher lecturer { get; set; }

        public int lecture_year { get; set; }

        public List<Student_has_lectures> lecture_Has_Students { get; set; }
    }
}
