using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;
using University.ViewModels;

namespace University.Profiles
{
    public class ApplicationProfile:Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
           
        }
    }
}
