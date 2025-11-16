using IntroEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntroEF.Controllers
{
    public class StudentController : Controller
    {
        Fall25_AEntities db = new Fall25_AEntities();

        // GET: Student

        [HttpGet]
        public ActionResult Create() { 
            return View(new Student());
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            db.Students.Add(s);
            db.SaveChanges();
            TempData["Msg"] = "Student " + s.Name + " Created";
            return RedirectToAction("List");
        }
        public ActionResult List(string search) {
            if (search != null) {
                var filter = (from s in db.Students
                            where s.Name.Contains(search)
                            select s).ToList();
                return View(filter);
            }
            var data = db.Students.ToList();
            return View(data);
        }
        public ActionResult Details(int id) {
            var data = db.Students.Find(id); //find only search with primary key
            return View(data);
        }
        [HttpGet]
        public ActionResult Update(int id) {
            var data = db.Students.Find(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Update(Student s) {
            var dbObj = db.Students.Find(s.Id);
            //s.Cgpa = dbObj.Cgpa;
            //db.Students.Remove(dbObj);
            //db.SaveChanges();
            db.Entry(dbObj).CurrentValues.SetValues(s);
            /*dbObj.Name = s.Name;
            dbObj.Email = s.Email;
            dbObj.Gender = s.Gender;*/
            db.SaveChanges();
            TempData["Msg"] = "Data Updated";
            return RedirectToAction("List");

        
        }
    }
}