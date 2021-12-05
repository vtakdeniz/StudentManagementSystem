using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string first_name { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string last_name { get; set; }
        [Required]
        [DisplayName("Age")]
        public int age { get; set; }
        public List<Lecture> lectures;
    }
}
