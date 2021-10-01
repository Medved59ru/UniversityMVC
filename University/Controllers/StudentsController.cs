using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.EFServise;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly EfServiceItem _item;

        public StudentsController(EfServiceItem item)
        {
            _item = item;
        }

        
        public IActionResult Index(int? id)
        {
           
            return View(_item.GetStudentsByGroup(id));
        }

           
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = _item.GetOneStudentBy(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = _item.GetListOfGroupsForDropDownMenu("Id", "Name", student);
              
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
            
            bool success = _item.AddOrEditStudent(student);
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
            
            var student = _item.GetOneStudentBy(id);
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
            
            var result = _item.RemoveStudentBy(id);
            if (result == false)
            {
                return RedirectToAction("Fail", "Main");
            }

            return RedirectToAction("Done", "Main");
        }

       
    }
}
