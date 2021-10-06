using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using University.Models;

namespace University.Serviñes
{
    public class CourseService
    {
        private readonly UniversityContext _context;

        public CourseService(UniversityContext context)
        {
           _context = context;
        }

        protected internal bool AddCourse(Course course)
        {
            bool success = false;
            if (course.Id == default)
            {
                _context.Add(course);
                success = true;
            }

            _context.SaveChanges();

            return success;
        }

        protected internal bool EditCourse(Course course)
        {
            bool success = false;
            if (course.Id != default)
            {
                _context.Update(course);
                success = true;
            }
            _context.SaveChanges();

            return success;
        }

       
        protected internal List<Course> GetListOfCourses()
        {
            return _context.Courses.ToList();
        }

        protected internal SelectList GetListOfCourseForDropDownMenu(string id = "Id", string name = "Name")
        {
            return new SelectList(_context.Courses, id, name);
        }

        protected internal SelectList GetListOfCourseForDropDownMenu(string id, string name, Group group)
        {
            return new SelectList(_context.Courses, id, name, group.Course);
        }

        protected internal Course GetOneCourseBy(int? id)
        {
        
            if (id == null)
            {
               return null;
            }
            else
            {
                return _context.Courses.Find(id);
            }
            
        }

        protected internal bool RemoveCourseBy(int id)
        {
            bool success = false;
            try
            {
                var course = _context.Courses.Find(id);
                _context.Courses.Remove(course);
                _context.SaveChanges();
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
