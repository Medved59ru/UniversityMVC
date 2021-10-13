using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using University.Models;
using University.ViewModels;
using AutoMapper;

namespace University.Services
{
    public class GroupService
    {
        private readonly UniversityContext _context;
        private readonly IMapper _mapper;

        public GroupService(UniversityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected internal bool AddGroup(GroupDto groupDto)
        {
            bool success;

            var group = _mapper.Map<Group>(groupDto);

            try
            {
                _context.Add(group);
                _context.SaveChanges();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        protected internal bool EditGroup(GroupDto groupDTO)
        {
            bool success = false;

            var group = _mapper.Map<Group>(groupDTO);

            if (group.Id != default)
            {
                _context.Update(group);
                _context.SaveChanges();
                success = true;
            }

            return success;
        }

        protected internal SelectList GetGroupsForDropDownMemu(string id = "Id", string name = "Name")
                => new SelectList(_context.Groups, id, name);

        protected internal SelectList GetListOfGroupsForDropDownMenu(Student student, string id = "Id", string name = "Name")
                => new SelectList(_context.Groups, id, name, student.Group);

        protected internal Group GetOneGroupOrDefualtBy(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return _context.Groups.Include(g => g.Course).FirstOrDefault(g => g.Id == id);

        }
            
        protected internal IQueryable<Group> GetGroupsOrDefualtBy(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return _context.Groups.Include(g => g.Course).Where(c => c.CourseId == id);
        }
                
        protected internal bool RemoveGrourBy(int id)
        {
            bool succces = false;
            if (StudentsExist(id))
            {
                succces = false;
            }
            else
            {
                _context.Groups.Remove(new Group() { Id = id });
                _context.SaveChanges();
                succces = true;
            }
            return succces;
        }

        private bool StudentsExist(int id)
        {
            bool result = true;
            if (_context.Students.Where(g => g.GroupId == id).Count() == 0)
            {
                result = false;
            }
            return result;
        }

    }
}
