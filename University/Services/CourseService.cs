using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using University.Models;
using University.ViewModels;
using AutoMapper;

namespace University.Serviñes
{
    public class CourseService
    {
        private readonly IMapper _mapper;
        private readonly UniversityContext _context;

        public CourseService(UniversityContext context, IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }

        protected internal bool AddCourse(CourseDTO courseDTO)
        {
            bool success;

            var course = _mapper.Map<Course>(courseDTO);
          
            _context.Add(course);
            _context.SaveChanges();
            success = true;
   
            return success;
        }

        protected internal bool EditCourse(CourseDTO courseDTO)
        {
            bool success;

            var course = _mapper.Map<Course>(courseDTO);
            try
            {
                _context.Update(course);
                _context.SaveChanges();
                success = true;
            }
            catch
            {
                success = false;
            }
             
            return success;
        }

       
        protected internal List<Course> GetListOfCourses()
                 =>_context.Courses.ToList();
        

        protected internal SelectList GetListOfCourseForDropDownMenu(string id = "Id", string name = "Name")
                 => new SelectList(_context.Courses, id, name);
      

        protected internal SelectList GetListOfCourseForDropDownMenu(Group group, string id = "Id", string name = "Name")
                 =>new SelectList(_context.Courses, id, name, group.Course);
       

        protected internal Course GetOneCourseOrDefaultBy(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return _context.Courses.Find(id);
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
