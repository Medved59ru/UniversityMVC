using Microsoft.AspNetCore.Mvc;
using University.EFServise;
using University.Models;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _item;

        public CoursesController(CourseService item)
        {
            _item = item;
        }

        public IActionResult Edit(int?id)
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
            return View(course);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }
            
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
            return View(course);
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
