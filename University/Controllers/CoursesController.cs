using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Serviсes;
using University.ViewModels;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;
      
        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var view = _courseService.GetOneCourseOrDefaultBy(id); ;
            if (view == null) return NotFound();
            return View(view);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] CourseDto courseDto)
        {
            if (id != courseDto.Id) return NotFound();

            bool success = _courseService.EditCourse(courseDto.Id, courseDto.Name);

            if (success)
                return RedirectToAction("Done", "Main");

            else
                return RedirectToAction("Fail", "Main");

        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var view = _courseService.GetOneCourseOrDefaultBy(id);
            if (view == null) return NotFound();
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
