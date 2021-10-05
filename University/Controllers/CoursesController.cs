using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.EFServise;
using University.Models;
using University.ViewModels;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _item;
        private readonly IMapper _mapper;

        public CoursesController(CourseService item, IMapper mapper)
        {
            _item = item;
            _mapper = mapper;
        }

        public IActionResult Edit(int?id)
        {
             
            if (id == null)
            {
                return NotFound();
            }
            var course = _item.GetOneCourseBy(id);
            var view = _mapper.Map<CourseDTO>(course);
            if (course == null)
            {
                return NotFound();
            }
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
            bool success = _item.EditCourse(course);
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
            if (id == null)
            {
                return NotFound();
            }
            
            var course = _item.GetOneCourseBy(id);

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
          
            var success = _item.RemoveCourseBy(id);
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
