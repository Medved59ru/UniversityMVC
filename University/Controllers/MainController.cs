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

        private readonly CourseService _course;
        private readonly GroupService _groupe;
        private readonly StudentService _student;

        public MainController(CourseService course, GroupService group, StudentService student)
        {
            _course = course;
            _groupe = group;
            _student = student;
        }

        public IActionResult Index()
        {
            return View(_course.GetListOfCourses());
        }

        [HttpGet]
        public IActionResult AddCourses()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourses(Course course)
        {
            var success = _course.AddCourse(course);
            if (success == true)
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

            ViewBag.Courses = _course.GetListOfCourseForDropDownMenu("Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult AddGroups(Group group)
        {

            var success = _groupe.AddGroup(group);
            if (success == true)
            {
                return RedirectToAction("Done");
            }
            return RedirectToAction("Fail");
        }

        [HttpGet]
        public IActionResult AddStudents()
        {
            ViewBag.Groups = _groupe.GetGroupsForDropDownMemu();
            return View();
        }

        public IActionResult AddStudents(Student student)
        {

            var success = _student.AddStudent(student);
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
