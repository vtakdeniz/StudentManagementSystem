using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

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

    }
}
