using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using University.Models;
using University.Serviсes;
using AutoMapper;
using University.ViewModels;
using System.Collections.Generic;

namespace University.Controllers
{
    public class MainController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CourseService _course;
        private readonly GroupService _group;
        private readonly StudentService _student;

        public MainController(CourseService course, GroupService group, StudentService student, IMapper mapper)
        {
            _mapper = mapper;
            _course = course;
            _group = group;
            _student = student;
        }

        public IActionResult Index()
        {
            var courses = _mapper.Map<IEnumerable<Course>, List<CourseDTO>>(_course.GetListOfCourses());
            return View(courses);
        }

        [HttpGet]
        public IActionResult AddCourses()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourses(CourseDTO courseDTO)
        {
            
            var success = _course.AddCourse(courseDTO);
            if (success)
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
            var list = _course.GetListOfCourses();
                 
            ViewBag.Courses = _course.GetListOfCourseForDropDownMenu();

            return View();
        }

        [HttpPost]
        public IActionResult AddGroups(GroupDTO groupDTO)
        {

            var success = _group.AddGroup(groupDTO);
            if (success)
            {
                return RedirectToAction("Done");
            }
            return RedirectToAction("Fail");
        }

        [HttpGet]
        public IActionResult AddStudents()
        {
            ViewBag.Groups = _group.GetGroupsForDropDownMemu();
            return View();
        }

        public IActionResult AddStudents(StudentDTO studentDTO)
        {

            var success = _student.AddStudent(studentDTO);
            if (success)
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
