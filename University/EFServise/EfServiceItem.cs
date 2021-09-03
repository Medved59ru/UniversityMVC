using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using University.Models;

namespace University.EFServise
{
    public class EfServiceItem
    {
        private readonly UniversityContext context;

        public EfServiceItem(UniversityContext context)
        {
            this.context = context;
        }


        protected internal bool AddOrEditCourse(Course course)
        {
            bool success = false;
            if (course.Id == default)
            {
                context.Add(course);
                success = true;
            }

            else
            {
                context.Update(course);
                success = true;
            }

            context.SaveChanges();

            return success;

        }

        protected internal bool AddOrEditGroup(Group group)
        {
            bool success = false;
            if (group.Id == default)
            {
                context.Add(group);
                success = true;
            }

            else
            {
                context.Update(group);
                success = true;
            }

            context.SaveChanges();

            return success;

        }

        protected internal bool AddOrEditStudent(Student student)
        {
            bool success = false;
            if (student.Id == default)
            {
                context.Entry(student).State = EntityState.Added;
                success = true;
            }

            else
            {
                context.Entry(student).State = EntityState.Modified;
                success = true;
            }

            context.SaveChanges();

            return success;

        }

        protected internal List<Course> GetListOfCourses()
        {
            return context.Courses.ToList();

        }

       protected internal SelectList GetListOfCourseForDropDownMenu(string id, string name)
        {

            return new SelectList(context.Courses, id, name);

        }

       protected internal SelectList GetListOfCourseForDropDownMenu(string id, string name, Group group)
        {

            return new SelectList(context.Courses, id, name, group.Course);

        }

        protected internal SelectList GetListOfGroupsForDropDownMenu(string id, string name, Student student)
        {

            return new SelectList(context.Groups, id, name, student.Group);

        }

       protected internal Course GetOneCourseBy(int? id)
        {
            return context.Courses.Find(id);
        }

       protected internal Group GetOneGroupBy(int? id)
        {
            Group group = new Group();
            group = context.Groups.Include(g => g.Course).FirstOrDefault(g => g.Id == id);
            return group;
        }

       protected internal Student GetOneStudentBy(int? id)
       {
            Student student = new Student();
            student = context.Students.Include(s => s.Group).FirstOrDefault(m => m.Id == id);
            return student;
       }
       
       protected internal Student GetOneStudentForDeleteBy(int? id)
        {
            Student student = new Student();
            student = context.Students.Include(s => s.Group).FirstOrDefault(m => m.Id == id);
            return student;
        }

        protected internal IQueryable<Group> GetGroupsBy(int? id)
       {
             
            return context.Groups.Include(g => g.Course).Where(c => c.CourseId == id);
        }

       protected internal IQueryable<Student> GetStudentsByGroup(int? id)
        {
            return context.Students.Include(s => s.Group).Where(g => g.GroupId == id);
        }

       protected internal bool RemoveCourseBy(int id)
        {
            bool success = false;
            try
            {
                var course = context.Courses.Find(id);
                context.Courses.Remove(course);
                context.SaveChanges();
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
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
                context.Groups.Remove(new Group() {Id= id});
                context.SaveChanges();
                succces = true;
            }
            
            return succces;

       }

        protected internal bool RemoveStudentBy(int id)
        {
            bool succces = false;
            try
            {
                context.Students.Remove(new Student() { Id = id });
                context.SaveChanges();
                succces = true;
            }
            catch
            {
                succces = false;
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
