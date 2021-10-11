using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using University.Models;
using University.ViewModels;
using AutoMapper;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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

        protected internal bool CreateCourse(string nameOfCourse)
        {
            if (string.IsNullOrWhiteSpace(nameOfCourse))
                throw new ArgumentException("Value can not be null or whitespace!", nameof(nameOfCourse));
          
            bool success;
            var course = new Course();
            course.Name = nameOfCourse;

            try
            {
                _context.Add(course);
                _context.SaveChanges();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        protected internal bool EditCourse(int id, string name)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value can not be null or whitespace!", nameof(name));

            bool success;
                
            var course = (Course)_context.Courses.FirstOrDefault(c => c.Id == id);
            course.Name = name;

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

       
        protected internal List<CourseDto> GetListOfCourses()
        {
            var view = _mapper.Map<IEnumerable<Course>, List<CourseDto>>(_context.Courses.ToList());
            return view;
        }
              
        

        protected internal SelectList GetListOfCourseForDropDownMenu(string id = "Id", string name = "Name")
        {
            return new SelectList(_context.Courses, id, name) ;
        }
                
      

        protected internal SelectList GetListOfCourseForDropDownMenu(Group group, string id = "Id", string name = "Name")
                 =>new SelectList(_context.Courses, id, name, group.Course);


        protected internal CourseDto GetOneCourseOrDefaultBy(int? id)
        {
           var course = _context.Courses.Find(id);
           CourseDto view = _mapper.Map<CourseDto>(course);
           return view;
        }

        protected internal bool RemoveCourseBy(int id)
        {
            bool success;
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
