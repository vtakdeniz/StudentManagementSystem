using System;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class ManagementContext :DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> opt) :base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.lecturer)
                .WithMany(t => t.lectures);
                
                
            ManyToManyRelationshipConfiguration(modelBuilder);
        }

        private void ManyToManyRelationshipConfiguration(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student_has_lectures>().
                HasKey(t => new { t.student_id, t.lecture_id });


            modelBuilder.Entity<Student_has_lectures>()
                .HasOne(sh => sh.lecture)
                .WithMany(st => st.lecture_Has_Students)
                .HasForeignKey(st => st.lecture_id);

            modelBuilder.Entity<Student_has_lectures>()
                .HasOne(sh => sh.student)
                .WithMany(st => st.student_Has_Lectures)
                .HasForeignKey(st => st.student_id);


        }


            public DbSet<Student> students { get; set; }
            public DbSet<Lecture> lectures { get; set; }
            public DbSet<Teacher> teachers { get; set; }
            public DbSet<Student_has_lectures> student_Has_Lectures{ get; set; }
            
    }
    
}
