using Microsoft.AspNetCore.Mvc;
using University.EFServise;
using University.Models;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UniversityContext _context;

        public CoursesController(UniversityContext context)
        {
            _context = context;
        }

           
        public IActionResult Edit(int?id)
        {
            EfServiceItem item = new EfServiceItem(_context);
            
            if (id == null)
            {
                return NotFound();
            }
            var course = item.GetOneCourseBy(id);
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
            EfServiceItem item = new EfServiceItem(_context);
            bool success = item.AddOrEditCourse(course);
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
            EfServiceItem item = new EfServiceItem(_context);
            var course = item.GetOneCourseBy(id);

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
            EfServiceItem item = new EfServiceItem(_context);
            var success = item.RemoveCourseBy(id);
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
