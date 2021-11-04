using Microsoft.AspNetCore.Mvc;
using StudentAdministration_v3.Data;
using StudentAdministration_v3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdministration_v3.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int pg = 1)
        {
            List<Student> students = _db.Students.ToList();

            const int pageSize = 5;

            if(pg < 1)
            {
                pg = 1;
            }
            int studentCount = students.Count();
            var pager = new Pager(studentCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = students.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Students.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var obj = _db.Students.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            else
            {
                _db.Students.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}
