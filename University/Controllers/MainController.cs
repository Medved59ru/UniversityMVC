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

       private readonly EfServiceItem _item;

        public MainController(EfServiceItem item)
        {
            _item = item;
        }

        public  IActionResult Index()
        {
           
            return View( _item.GetListOfCourses());

        }

        [HttpGet]
        public IActionResult AddCourses()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddCourses(Course course)
        {
            var success = _item.AddOrEditCourse(course);
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
           
            ViewBag.Courses =  _item.GetListOfCourseForDropDownMenu("Id", "Name");
            
            return View();
        }

        [HttpPost]
        public IActionResult AddGroups(Group group)
        {
            
            var success = _item.AddOrEditGroup(group);
            if (success == true)
            {
                return RedirectToAction("Done");
            }
            return RedirectToAction("Fail");
        }

        [HttpGet]
        public IActionResult AddStudents()
        {
            ViewBag.Groups = _item.GetGroupsForDropDownMemu();
            return View();
        }


        public IActionResult AddStudents(Student student)
        {
           
            var success = _item.AddOrEditStudent(student);
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
