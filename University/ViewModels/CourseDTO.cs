using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class CourseDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Õ¿«¬¿Õ»≈  ”–—¿")]
        [Required]
        public string Name { get; set; }

        
    }
}

