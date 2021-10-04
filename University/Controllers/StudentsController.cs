using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.EFServise;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _student;
        private readonly GroupService _group;

        public StudentsController(StudentService student, GroupService group)
        {
            _student = student;
            _group = group;
        }

        public IActionResult Index(int? id)
        {

            return View(_student.GetStudentsByGroup(id));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = _student.GetOneStudentBy(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = _group.GetListOfGroupsForDropDownMenu("Id", "Name", student);

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

            bool success = _student.EditStudent(student);
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

            var student = _student.GetOneStudentBy(id);
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

            var result = _student.RemoveStudentBy(id);
            if (result == false)
            {
                return RedirectToAction("Fail", "Main");
            }

            return RedirectToAction("Done", "Main");
        }

    }
}
