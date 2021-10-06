using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Serviсes;
using University.Models;
using University.ViewModels;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(CourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public IActionResult Edit(int?id)
        {
             
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetOneCourseBy(id);
            if (course == null)
            {
                return NotFound();
            }
            var view = _mapper.Map<CourseDTO>(course);
            return View(view);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] CourseDTO courseDTO)
        {
            if (id != courseDTO.Id)
            {
                return NotFound();
            }

            var course = _mapper.Map<Course>(courseDTO);
            bool success = _courseService.EditCourse(course);
            if (success == true)
            {
                return RedirectToAction("Done", "Main");
            }
            else
            {
                return RedirectToAction("Fail", "Main");
            }
        }

        
        public IActionResult Delete(int? id)
        {
                        
            var course = _courseService.GetOneCourseBy(id);

            if (course == null)
            {
                return NotFound();
            }
            var view = _mapper.Map<CourseDTO>(course);
            return View(view);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
          
            var success = _courseService.RemoveCourseBy(id);
            if (success == true)
            {
                return RedirectToAction("Done", "Main");
            }
            else
            {
                return RedirectToAction("Fail", "Main");
            }
                
        }

    }
}
