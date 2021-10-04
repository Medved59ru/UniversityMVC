using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Display(Name = "Название группы")]
        public string Name { get; set; }

        public int CourseId { get; set; }
        [Display(Name = "Курс")]
        public Course Course { get; set; }
    }
}
