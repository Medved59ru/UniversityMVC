using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class StudentDto
    {
        public int Id { get; set; }

        [Display(Name = "ИМЯ")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "ОТЧЕСТВО")]
        [Required]
        public string MidName { get; set; }

        [Display(Name = "ФАМИЛИЯ")]
        [Required]
        public string LastName { get; set; }

        public int GroupId { get; set; }

        public GroupDto Group { get; set; }
    }
}
