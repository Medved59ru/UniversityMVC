using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class CourseDTO
    {
        public int Id { get; set; }
        
        [Display(Name = "�������� �����")]
        public string Name { get; set; }

        
    }
}

