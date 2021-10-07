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

        public IActionResult Edit(int? id)
        {
         
            var course = _courseService.GetOneCourseOrDefaultBy(id);

            if (course == null)
               return NotFound();
         
            var view = _mapper.Map<CourseDTO>(course);

            return View(view);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] CourseDTO courseDTO)
        {
            if (id != courseDTO.Id)
               return NotFound();
            
            bool success = _courseService.EditCourse(courseDTO);

            if (success)
               return RedirectToAction("Done", "Main");
          
            else
               return RedirectToAction("Fail", "Main");
          
        }

        
        public IActionResult Delete(int? id)
        {
            var course = _courseService.GetOneCourseOrDefaultBy(id);

            if (course == null)
                return NotFound();
         
            var view = _mapper.Map<CourseDTO>(course);

            return View(view);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
          
            var success = _courseService.RemoveCourseBy(id);
          
            if (success)
               return RedirectToAction("Done", "Main");
           
            else
               return RedirectToAction("Fail", "Main");
             
        }

    }
}
