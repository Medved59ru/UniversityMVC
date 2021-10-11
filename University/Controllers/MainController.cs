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
        private readonly CourseService _courseService;
        private readonly GroupService _group;
        private readonly StudentService _student;

        public MainController(CourseService courseService, GroupService group, StudentService student, IMapper mapper)
        {
            _mapper = mapper;
            _courseService = courseService;
            _group = group;
            _student = student;
        }

        public IActionResult Index()
        {
            var view = _courseService.GetListOfCourses();
            return View(view);
        }

        [HttpGet]
        public IActionResult AddCourses()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourses(CourseDto courseDto)
        {
            
            var success = _courseService.CreateCourse(courseDto.Name);
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
            var list = _courseService.GetListOfCourses();

            ViewBag.Courses = _courseService.GetListOfCourseForDropDownMenu();

            return View();
        }

        [HttpPost]
        public IActionResult AddGroups(GroupDto groupDto)
        {

            var success = _group.AddGroup(groupDto);
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

        public IActionResult AddStudents(StudentDto studentDTO)
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
