using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Название курса")]
        [Required]
        public string Name { get; set; }
    }
}
