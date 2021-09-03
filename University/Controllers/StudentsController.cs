using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.EFServise;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        
        public IActionResult Index(int? id)
        {
            EfServiceItem item = new EfServiceItem(_context);
            return View(item.GetStudentsByGroup(id));
        }

           
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EfServiceItem item = new EfServiceItem(_context);
            var student = item.GetOneStudentBy(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = item.GetListOfGroupsForDropDownMenu("Id", "Name", student);
              
            return View(student);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,MidName,LastName,GroupId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            EfServiceItem item = new EfServiceItem(_context);
            bool success = item.AddOrEditStudent(student);
            if (success==true)
            {
                return RedirectToAction("Done","Main");
            }
            return RedirectToAction("Fail", "Main");
            
        }

        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EfServiceItem item = new EfServiceItem(_context);
            var student = item.GetOneStudentBy(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EfServiceItem item = new EfServiceItem(_context);
            var result = item.RemoveStudentBy(id);
            if (result == false)
            {
                return RedirectToAction("Fail", "Main");
            }

            return RedirectToAction("Done", "Main");
        }

       
    }
}
