using alsyedAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace alsyedAcademy.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<Course> allCourses = db.Courses.ToList();
            /*List<CourseOutlineViewModel> courseAndOutlines = new List<CourseOutlineViewModel>();
            foreach(Course course in allCourses)
            {
                CourseOutlineViewModel model = new CourseOutlineViewModel() { id = course.id, name = course.name };
                try
                {
                    List<Outline> outlines = db.Outlines.Where(x => x.cid == course.id).ToList();
                    if (outlines != null) {
                        model.outlines = outlines;
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    int a = 8; // just for checking ex.messge to apply break
                    //do noting
                }
                courseAndOutlines.Add(model);               
                                
            }
            return View(courseAndOutlines);
            */
            List<News> news = db.News.ToList();
            ViewBag.news = news;
            ViewBag.Title = "come to learn go to earn";
            return View(allCourses);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}