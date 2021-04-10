using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsListMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsListMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Student Student { get; set; }
        public StudentsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Student = new Student();
            if (id == null)
            {
                //Create request
                return View(Student);
            }
            //Update request
            Student = _db.Students.FirstOrDefault(u => u.Id == id);
            if (Student == null)
            {
                return NotFound();
            }
            return View(Student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Student.Id == 0)
                {
                    //Create request
                    _db.Students.Add(Student);
                }
                else
                {
                    //Update request
                    _db.Students.Update(Student);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Student);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Students.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var studentFromDb = await _db.Students.FirstOrDefaultAsync(u => u.Id == id);
            if (studentFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Students.Remove(studentFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Record Deleted Successfully" });
        }
        #endregion
    }
}
