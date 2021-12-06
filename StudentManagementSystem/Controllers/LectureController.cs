using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class LectureContoller:Controller
    {
        private readonly ManagementContext _db;

        public LectureContoller(ManagementContext db)
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
        public IActionResult Create(Lecture lecture)
        {
            if (ModelState.IsValid) {

                if (_db.lectures.FirstOrDefault(le => le.classroom_code == lecture.classroom_code) != null) {
                    TempData["isClassroomCodeDuplicate"] = true;
                    return View(lecture);
                }

                _db.lectures.Add(lecture);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }
            var lecture = _db.students.FirstOrDefault(le => le.Id == id);
            if (lecture == null) {
                return NotFound();
            }
            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Lecture lecture)
        {
            if (ModelState.IsValid) {
                _db.lectures.Update(lecture);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }

            var lecture = _db.lectures.FirstOrDefault(le => le.Id == id);
            if (lecture == null) {
                return NotFound();
            }
            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLecture(int? id) {
            var lecture = _db.lectures.FirstOrDefault(le=>le.Id==id);

            if (lecture == null) {
                return NotFound();
            }
            _db.lectures.Remove(lecture);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
