using System.Linq;
using University.Models;

namespace University
{
    public static class SampleData
    {
        public static void Initialize(UniversityContext context)
        {
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course { Name = "C# START" },
                    new Course { Name = "C# MIDL" },
                    new Course { Name = "C# MENTORING" }
                    );
                context.SaveChanges();
            }

            if (!context.Groups.Any())
            {
                context.Groups.AddRange(
                    new Group
                    {
                        Name = "SR-01",
                        CourseId = 1

                    },
                    new Group
                    {
                        Name = "SR-02",
                        CourseId = 2
                    },
                     new Group
                     {
                         Name = "SR-03",
                         CourseId = 3
                     }

                    );
                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student
                    {
                        Name = "Иван", 
                        MidName = "Иванович", 
                        LastName = "Иванов",
                        GroupId = 1
                    },
                    new Student
                    {
                        Name = "Петр",
                        MidName = "Иванович",
                        LastName = "Иванов",
                        GroupId = 2
                    },
                    new Student
                    {
                        Name = "Иван",
                        MidName = "Иванович",
                        LastName = "Сидоров",
                        GroupId = 3
                    }
                    );
                context.SaveChanges();
            }

        }
    }
}
