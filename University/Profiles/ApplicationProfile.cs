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
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Course, SelectListItem>().ForMember(
                dest=>dest.Value, opt =>opt.MapFrom(src=>src.Id.ToString())
                )
                .ForMember(
                dest=>dest.Text, opt => opt.MapFrom(src=>src.Name)
                );
        }
    }
}
