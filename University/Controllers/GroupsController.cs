using System.Linq;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.Serviсes;
using AutoMapper;
using System.Collections.Generic;
using University.ViewModels;

namespace University.Controllers
{
    public class GroupsController : Controller
    {
        private readonly GroupService _groupService;
        private readonly CourseService _courseService;
        private readonly IMapper _mapper;

        public GroupsController(GroupService group, CourseService course, IMapper mapper)
        {
            _groupService = group;
            _courseService = course;
            _mapper = mapper;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var list = _groupService.GetGroupsBy(id).ToList();
            var view = _mapper.Map<List<Group>, List<GroupDTO>>(list);

            return View(view);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = _groupService.GetOneGroupBy(id);

            if (group == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = _courseService.GetListOfCourseForDropDownMenu("Id", "Name", group);

            var view = _mapper.Map<GroupDTO>(group);

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,CourseId")] GroupDTO groupDTO)
        {
            if (id != groupDTO.Id)
            {
                return NotFound();
            }

            var group = _mapper.Map<Group>(groupDTO);

            bool success = _groupService.EditGroup(group);

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

            var group = _groupService.GetOneGroupBy(id);

            if (group == null)
            {
                return NotFound();
            }

            var view = _mapper.Map<GroupDTO>(group);

            return View(view);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var result = _groupService.RemoveGrourBy(id);
            if (result == false)
            {
                return RedirectToAction("Fail", "Main");
            }

            return RedirectToAction("Done", "Main");
        }

    }
}
