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
        private readonly StudentService _studentSevice;
        private readonly GroupService _groupService;
        private readonly IMapper _mapper;

        public StudentsController(StudentService student, GroupService group, IMapper mapper)
        {
            _studentSevice = student;
            _groupService = group;
            _mapper = mapper;
        }

        public IActionResult Index(int? id)
        {
            var list = _studentSevice.GetStudentsByGroup(id).ToList();
            var view = _mapper.Map<List<Student>, List<StudentDto>>(list);

            return View(view);
        }

        public IActionResult Edit(int? id)
        {
            var student = _studentSevice.GetOneStudentOrDefaultBy(id);

            if (student == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = _groupService.GetListOfGroupsForDropDownMenu(student, "Id", "Name");

            var view = _mapper.Map<StudentDto>(student);

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,MidName,LastName,GroupId")] StudentDto studentDTO)
        {
            if (id != studentDTO.Id)
                 return NotFound();
          
            bool success = _studentSevice.EditStudent(studentDTO);

            if (success)
            {
                return RedirectToAction("Done", "Main");
            }
            return RedirectToAction("Fail", "Main");

        }

        public IActionResult Delete(int? id)
        {
            var student = _studentSevice.GetOneStudentOrDefaultBy(id);

            if (student == null)
                return NotFound();
            
            var view = _mapper.Map<StudentDto>(student);

            return View(view);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var result = _studentSevice.RemoveStudentBy(id);

            if (!result)
            {
                return RedirectToAction("Fail", "Main");
            }
            return RedirectToAction("Done", "Main");
        }

    }
}
