using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training.Infrastructure;
using Training.ViewModels;
using Training.Models;
namespace Training.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext dbContext = new AppDbContext();
        private int pageSize = 5;
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            var trainings = dbContext.Trainings.AsEnumerable<Training.Models.Training>().Skip((page - 1) * pageSize).Take(pageSize);
            var indexModel = new IndexViewModel()
            {
                PageInfo = new PageInfo()
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = trainings.Count()
                },
                Trainings = trainings
            };
            return View(indexModel);
        }
        [HttpGet]
        public ActionResult Training(int? id)
        {
            var training = dbContext.Trainings.FirstOrDefault(t => t.Id == id);
            if (training == null)
            {
                return HttpNotFound("Training is not found");
            }
            ViewBag.Training = training;
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Value = "-1",
                Text = "Выберите университет"
            });
            list.Add(new SelectListItem() { Value = "BSUIR", Text = "BSUIR" });
            list.Add(new SelectListItem() { Value = "BSU", Text = "BSU" });
            list.Add(new SelectListItem() { Value = "BSEU", Text = "BSEU" });
            ViewBag.UniList = list;
            return View();
        }
        [HttpPost]
        public ActionResult Training(Student student)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["id"];
                var tr = dbContext.Trainings.FirstOrDefault(t => t.Id == id);
                var st = dbContext.Students.FirstOrDefault(s => s.FirstName == student.FirstName
                && s.LastName == student.LastName && s.University == student.University);
                if (st != null)
                {
                    st.Trainings.Add(tr);
                    dbContext.Entry<Student>(st).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    student.Trainings = new List<Models.Training>() { tr };
                    dbContext.Students.Add(student);
                }
                dbContext.SaveChanges();
                @TempData["Congrats"] = "Вы успешно зарегистрировались";
                return RedirectToAction("Training", new { id = id });                          
            }
            return View(student);
        }
       
        public ActionResult RegStudents(int? id)
        {
            var students = dbContext.Trainings.FirstOrDefault(t => t.Id == id)?.Students?.ToList();
            if (students == null)
                students = new List<Student>();  
            return PartialView("_students", students);
        }

    }
}