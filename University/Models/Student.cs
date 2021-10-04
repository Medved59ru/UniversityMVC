using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name ="Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string MidName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        public int GroupId { get; set; }
        [Display(Name = "Группа")]
        public Group Group { get; set; }
    }
}
