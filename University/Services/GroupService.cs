using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using University.Models;

namespace University.Serviñes
{
    public class GroupService
    {
        private readonly UniversityContext context;

        public GroupService(UniversityContext context)
        {
            this.context = context;
        }

        protected internal bool AddGroup(Group group)
        {
            bool success = false;
            if (group.Id == default)
            {
                context.Add(group);
                success = true;
            }

            context.SaveChanges();

            return success;
        }

        protected internal bool EditGroup(Group group)
        {
            bool success = false;
            if (group.Id != default)
            {
                context.Update(group);
                success = true;
            }

            context.SaveChanges();

            return success;
        }

        protected internal SelectList GetGroupsForDropDownMemu(string id = "Id", string name = "Name")
            => new SelectList(context.Groups, id, name);

        protected internal SelectList GetListOfGroupsForDropDownMenu(string id, string name, Student student)
             => new SelectList(context.Groups, id, name, student.Group);
       
        protected internal Group GetOneGroupBy(int? id)
        {
            Group group = new Group();
            group = context.Groups.Include(g => g.Course).FirstOrDefault(g => g.Id == id);
            return group;
        }

        protected internal IQueryable<Group> GetGroupsBy(int? id)
        {
            return context.Groups.Include(g => g.Course).Where(c => c.CourseId == id);
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
                context.Groups.Remove(new Group() { Id = id });
                context.SaveChanges();
                succces = true;
            }

            return succces;
        }

        private bool StudentsExist(int id)
        {
            bool result = true;
            if (context.Students.Where(g => g.GroupId == id).Count() == 0)
            {
                result = false;
            }
            return result;
        }

    }
}
