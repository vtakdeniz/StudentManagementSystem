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
    public class LectureController : Controller
    {
        private readonly ManagementContext _db;

        public LectureController(ManagementContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Lecture> lectures = _db.lectures.Include(le => le.lecturer).ToList();
            return View(lectures);
        }

        public IActionResult Create()
        {
            var teachers = _db.teachers.ToList();
            ViewBag.teachers = teachers;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LectureViewModel createLectureDto)
        {
            var teachers = _db.teachers.ToList();
            ViewBag.teachers = teachers;

            Lecture lecture= new Lecture { lecture_name=createLectureDto.lecture_name,classroom_code=createLectureDto.classroom_code};
            var teacher = _db.teachers.FirstOrDefault(te=>te.Id==createLectureDto.teacher_id);
            
            if (teacher==null) {
                return NotFound();
            }

            lecture.lecturer = teacher;

            if (ModelState.IsValid)
            {

                if (_db.lectures.FirstOrDefault(le => le.classroom_code == lecture.classroom_code) != null)
                {
                    TempData["isClassroomCodeDuplicate"] = true;
                    return View(createLectureDto);
                }
                
                _db.lectures.Add(lecture);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createLectureDto);
        }

        public IActionResult Edit(int? id)
        {
            var teachers = _db.teachers.ToList();
            ViewBag.teachers = teachers;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            TempData["editLectureId"] = id;
            var lecture = _db.lectures.Include(le=>le.lecturer).FirstOrDefault(st => st.Id == id);
            if (lecture == null)
            {
                return NotFound();
            }
            var lectureDto = new LectureViewModel { lecture_name = lecture.lecture_name, classroom_code = lecture.classroom_code, teacher_id = lecture.lecturer.Id };
            return View(lectureDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LectureViewModel lectureDto)
        {
            var teachers = _db.teachers.ToList();
            ViewBag.teachers = teachers;

            if (_db.lectures.FirstOrDefault(le => le.classroom_code == lectureDto.classroom_code) != null)
            {
                TempData["isClassroomCodeDuplicate"] = true;
                return View(lectureDto);
            }

            int id =(int)TempData["editLectureId"];
            var lecture=_db.lectures.Find(id);
            lecture.classroom_code = lectureDto.classroom_code;
            lecture.lecture_name = lectureDto.lecture_name;

            var teacher = _db.teachers.FirstOrDefault(te => te.Id == lectureDto.teacher_id);
            if (teacher == null) {
                return NotFound();
            }
            lecture.lecturer = teacher;
            if (ModelState.IsValid)
            {
                _db.lectures.Update(lecture);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lectureDto);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var lecture = _db.lectures.FirstOrDefault(st => st.Id == id);
            if (lecture == null)
            {
                return NotFound();
            }
            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLecture(int? id)
        {
            var lecture = _db.lectures.FirstOrDefault(st => st.Id == id);

            if (lecture == null)
            {
                return NotFound();
            }
            _db.lectures.Remove(lecture);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
