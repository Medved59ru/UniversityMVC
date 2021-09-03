using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Models;
using University.EFServise;

namespace University.Controllers
{
    public class GroupsController : Controller
    {
        private readonly UniversityContext _context;

        public GroupsController(UniversityContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EfServiceItem item = new EfServiceItem(_context);
            return View(item.GetGroupsBy(id).ToList());
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EfServiceItem item = new EfServiceItem(_context);
           
           var group = item.GetOneGroupBy(id);
           
            if (group == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = item.GetListOfCourseForDropDownMenu("Id", "Name", group);
          
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Name,CourseId")] Group group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            EfServiceItem item = new EfServiceItem(_context);
            bool success  = item.AddOrEditGroup(group);

            if (success == true)
            { 
                return RedirectToAction("Done", "Main");
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
            var group = item.GetOneGroupBy(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EfServiceItem item = new EfServiceItem(_context);
            var result = item.RemoveGrourBy(id);
            if (result == false)
            {
              return RedirectToAction("Fail", "Main");
            }
           
            return RedirectToAction("Done", "Main");
        }

    }
}
