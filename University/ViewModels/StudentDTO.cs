using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.ViewModels
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MidName { get; set; }

        public string LastName { get; set; }

        public int GroupId { get; set; }

        public GroupDTO Group { get; set; }
    }
}
