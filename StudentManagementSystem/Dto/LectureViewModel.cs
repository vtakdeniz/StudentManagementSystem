using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Dto
{
    public class LectureViewModel
    {

        [Required]
        [DisplayName("Lecture Name")]
        public string lecture_name { get; set; }
        [Required]
        [DisplayName("Classroom Code")]
        public string classroom_code { get; set; }

        public int teacher_id { get; set; }

        public int lecture_year { get; set; }

    }
}
