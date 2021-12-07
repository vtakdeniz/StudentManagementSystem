using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Dto;

namespace StudentManagementSystem.Controllers
{
    public class StudentController:Controller
    {
        private readonly ManagementContext _db;

        public StudentController(ManagementContext db)
        {
            _db = db;
        }

        public IActionResult Index() {
            List<Student> students = _db.students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid) {

                if (_db.students.FirstOrDefault(st => st.school_number == student.school_number) != null) {
                    TempData["isSchoolNumberDuplicate"] = true; 
                    return View(student);
                }

                _db.students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }
            var student = _db.students.FirstOrDefault(st => st.Id == id);
            if (student == null) {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid) {
                _db.students.Update(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }

            var student = _db.students.FirstOrDefault(st => st.Id == id);
            if (student == null) {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStudent(int? id) {
            var student = _db.students.FirstOrDefault(st=>st.Id==id);

            if (student == null) {
                return NotFound();
            }
            _db.students.Remove(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Inspect(int? id) {
            var student = _db.students.Include(st=>st.student_Has_Lectures).ThenInclude(sh=>sh.lecture).ThenInclude(le=>le.lecturer).FirstOrDefault(st => st.Id == id);
            var studentDto = new InspectStudentViewModel
            {
                Id=student.Id,
                school_number = student.school_number,
                first_name = student.first_name,
                last_name = student.last_name,
                class_year = student.class_year,
                enrollment_date = student.enrollment_date,
                age = student.age,
                student_Has_Lectures = student.student_Has_Lectures
            };

            if (student == null)
            {
                return NotFound();
            }
            return View(studentDto);
        }

        public IActionResult AddClassInspect(int? id)
        {
            var student = _db.students.Include(st => st.student_Has_Lectures).ThenInclude(sh => sh.lecture).ThenInclude(le => le.lecturer).FirstOrDefault(st => st.Id == id);
            var studentDto = new InspectStudentViewModel
            {
                Id=student.Id,
                school_number = student.school_number,
                first_name = student.first_name,
                last_name = student.last_name,
                class_year = student.class_year,
                enrollment_date = student.enrollment_date,
                age = student.age,
                student_Has_Lectures = student.student_Has_Lectures
            };
            
            var lectures = _db.lectures.ToList();
            if (lectures != null) {
                foreach (var element in student.student_Has_Lectures) {
                    var lecture_to_remove=lectures.Find(lc=>lc.Id==element.lecture_id);
                    if (lecture_to_remove != null) {
                        lectures.Remove(lecture_to_remove);
                    }
                }
                ViewBag.lectures = lectures;
            }

            if (student == null)
            {
                return NotFound();
            }
            TempData["isAddingTrue"] = true;
            return View("Inspect",studentDto);
        }
            

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLectureToStudent(InspectStudentViewModel inspectStudentViewModel) {

            if (inspectStudentViewModel.lecture_id==0) {
                return RedirectToAction("Index");
            }
        
            var student = _db.students.Include(st => st.student_Has_Lectures).ThenInclude(sh => sh.lecture).ThenInclude(le => le.lecturer).FirstOrDefault(st => st.Id == inspectStudentViewModel.Id);
            var lecture = _db.lectures.FirstOrDefault(le => le.Id == inspectStudentViewModel.lecture_id);
            if (lecture == null) {
                return NotFound();
            }
            if (lecture.lecture_year > student.class_year) {
                TempData["isStudentYearMismatches"] = true;
                return RedirectToAction("Inspect",new { id=student.Id});
            }
            foreach (var element in student.student_Has_Lectures)
            {
                if (lecture.Id == element.lecture_id) {
                    return BadRequest();
                }
            }
            
            var student_has_lecture = new Student_has_lectures { student_id=student.Id,student=student,lecture_id=lecture.Id,lecture=lecture};
            _db.student_Has_Lectures.Add(student_has_lecture);

            _db.students.Update(student);
            _db.SaveChanges();

            student = _db.students.Include(st => st.student_Has_Lectures).ThenInclude(sh => sh.lecture).ThenInclude(le => le.lecturer).FirstOrDefault(st => st.Id == inspectStudentViewModel.Id);

            var studentDto = new InspectStudentViewModel
            {
                Id = student.Id,
                school_number = student.school_number,
                first_name = student.first_name,
                last_name = student.last_name,
                class_year = student.class_year,
                enrollment_date = student.enrollment_date,
                age = student.age,
                student_Has_Lectures = student.student_Has_Lectures
            };
            return View("Inspect", studentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLecture([FromForm]int lecture_id, [FromForm] int student_id) {
            var relation = _db.student_Has_Lectures.FirstOrDefault(r=>r.lecture_id==lecture_id || r.student_id==student_id );
            if (relation == null) {
                return NotFound();
            }

            _db.student_Has_Lectures.Remove(relation);
            _db.SaveChanges();
            return RedirectToAction("Inspect",new { id=student_id });
            
        }

    }
}
