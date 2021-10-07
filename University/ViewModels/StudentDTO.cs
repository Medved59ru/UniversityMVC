using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Display(Name = "ИМЯ")]
        public string Name { get; set; }

        [Display(Name = "ОТЧЕСТВО")]
        public string MidName { get; set; }

        [Display(Name = "ФАМИЛИЯ")]
        public string LastName { get; set; }

        public int GroupId { get; set; }

        public GroupDTO Group { get; set; }
    }
}
