using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class GroupDto
    {
        public int Id { get; set; }

        [Display(Name = "ГРУППА")]
        public string Name { get; set; }

        public int CourseId { get; set; }
    
        public CourseDto Course { get; set; }

        
    }
}
