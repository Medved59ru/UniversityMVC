using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using University.Models;
using University.EFServise;


namespace University.Controllers
{
    public class MainController : Controller
    {

        UniversityContext _context;

        public MainController(UniversityContext context)
        {
            _context = context;
        }

        public  IActionResult Index()
        {
            EfServiceItem item = new EfServiceItem(_context);
            return View( item.GetListOfCourses());

        }

        [HttpGet]
        public IActionResult AddCourses()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddCourses(Course course)
        {
            EfServiceItem item = new EfServiceItem(_context);
            var success = item.AddOrEditCourse(course);
            if(success == true)
            {
                return RedirectToAction("Done");
            }
            return RedirectToAction("Fail");
        }

        

        public ActionResult Done()
        {
            return View();
        }




        [HttpGet]
        public IActionResult AddGroups()
        {
            EfServiceItem items = new EfServiceItem(_context);
            ViewBag.Courses =  items.GetListOfCourseForDropDownMenu("Id", "Name");
            
            return View();
        }

        [HttpPost]
        public IActionResult AddGroups(Group group)
        {
            EfServiceItem item = new EfServiceItem(_context);
            var success = item.AddOrEditGroup(group);
            if (success == true)
            {
                return RedirectToAction("Done");
            }
            return RedirectToAction("Fail");
        }

        [HttpGet]
        public IActionResult AddStudents()
        {
            SelectList groups = new SelectList(_context.Groups, "Id", "Name");
            ViewBag.Groups = groups;
            return View();
        }


        public IActionResult AddStudents(Student student)
        {
            EfServiceItem item = new EfServiceItem(_context);
            var success = item.AddOrEditStudent(student);
            if (success == true)
            {
                return RedirectToAction("Done");
            }
            return RedirectToAction("Fail");

        }

       
        public IActionResult Fail()
        {
            return View();
        }


    }
}
