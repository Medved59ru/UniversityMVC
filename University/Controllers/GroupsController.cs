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

        public GroupsController(GroupService groupService, CourseService courseService, IMapper mapper)
        {
            _groupService = groupService;
            _courseService = courseService;
            _mapper = mapper;
        }

        public IActionResult Index(int? id)
        {
            
            var list = _groupService.GetGroupsOrDefualtBy(id).ToList();
            var view = _mapper.Map<List<Group>, List<GroupDto>>(list);

            return View(view);
        }

        public IActionResult Edit(int? id)
        {
            var group = _groupService.GetOneGroupOrDefualtBy(id);

            if (group == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = _courseService.GetListOfCourseForDropDownMenu(group,"Id", "Name");

            var view = _mapper.Map<GroupDto>(group);

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,CourseId")] GroupDto groupDTO)
        {
            if (id != groupDTO.Id)
            {
                return NotFound();
            }

            bool success = _groupService.EditGroup(groupDTO);

            if (success == true)
            {
                return RedirectToAction("Done", "Main");
            }
            return RedirectToAction("Fail", "Main");
        }

        public IActionResult Delete(int? id)
        {
           
            var group = _groupService.GetOneGroupOrDefualtBy(id);

            if (group == null)
            {
                return NotFound();
            }

            var view = _mapper.Map<GroupDto>(group);

            return View(view);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var result = _groupService.RemoveGrourBy(id);
            if (!result)
            {
                return RedirectToAction("Fail", "Main");
            }

            return RedirectToAction("Done", "Main");
        }

    }
}
