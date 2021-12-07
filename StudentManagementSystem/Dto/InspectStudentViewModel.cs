using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Dto
{
    public class InspectStudentViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("School number")]
        [Range(0, int.MaxValue, ErrorMessage = "Student's number cannot be negative.")]
        public int school_number { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string first_name { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string last_name { get; set; }
        [Required]
        [DisplayName("Class Year")]
        [Range(1, 12, ErrorMessage = "Student's class year can only be between 1 and 12.")]
        public int class_year { get; set; }
        [DisplayName("Enrollment Date")]
        public DateTime enrollment_date { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Age")]
        [Range(7, 25, ErrorMessage = "Student's age cannot be smaller than 7, and bigger than 25.")]
        public int age { get; set; }

        public List<Student_has_lectures> student_Has_Lectures { get; set; }

        public int lecture_id { get; set; }
    }
}
