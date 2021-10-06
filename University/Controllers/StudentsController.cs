using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.Serviсes;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using University.ViewModels;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _student;
        private readonly GroupService _group;
        private readonly IMapper _mapper;

        public StudentsController(StudentService student, GroupService group, IMapper mapper)
        {
            _student = student;
            _group = group;
            _mapper = mapper;
        }

        public IActionResult Index(int? id)
        {
            var list = _student.GetStudentsByGroup(id).ToList();
            var view = _mapper.Map<List<Student>, List<StudentDTO>>(list);

            return View(view);
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
            var view = _mapper.Map<StudentDTO>(student);

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,MidName,LastName,GroupId")] StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
            {
                return NotFound();
            }

            var student = _mapper.Map<Student>(studentDTO);
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

            var view = _mapper.Map<StudentDTO>(student);

            return View(view);
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
