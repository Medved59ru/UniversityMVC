using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.ViewModels
{
    public class GroupDTO
    {
        public int Id { get; set; }
    
        [Display(Name = "ГРУППА")]
        public string Name { get; set; }

        public int CourseId { get; set; }
    
        public CourseDTO Course { get; set; }

        public string SelectedItemId { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }
    }
}
