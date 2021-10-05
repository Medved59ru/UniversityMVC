using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using University.Models;

namespace University.EFServise
{
    public class CourseService
    {
        private readonly UniversityContext context;

        public CourseService(UniversityContext context)
        {
            this.context = context;
        }

        protected internal bool AddCourse(Course course)
        {
            bool success = false;
            if (course.Id == default)
            {
                context.Add(course);
                success = true;
            }

            context.SaveChanges();

            return success;
        }

        protected internal bool EditCourse(Course course)
        {
            bool success = false;
            if (course.Id != default)
            {
                context.Update(course);
                success = true;
            }
            context.SaveChanges();

            return success;
        }

       
        protected internal List<Course> GetListOfCourses()
        {
            return context.Courses.ToList();
        }

        protected internal SelectList GetListOfCourseForDropDownMenu(string id = "Id", string name = "Name")
        {
            return new SelectList(context.Courses, id, name);
        }

        protected internal SelectList GetListOfCourseForDropDownMenu(string id, string name, Group group)
        {
            return new SelectList(context.Courses, id, name, group.Course);
        }

        protected internal Course GetOneCourseBy(int? id)
        {
            return context.Courses.Find(id);
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

    }
}
