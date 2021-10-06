using Microsoft.EntityFrameworkCore;
using System.Linq;
using University.Models;

namespace University.Serviñes
{
    public class StudentService
    {
        private readonly UniversityContext context;

        public StudentService(UniversityContext context)
        {
            this.context = context;
        }

        protected internal bool AddStudent(Student student)
        {
            bool success = false;
            if (student.Id == default)
            {
                context.Entry(student).State = EntityState.Added;
                success = true;
            }

            else
            {
                context.Entry(student).State = EntityState.Modified;
                success = true;
            }

            context.SaveChanges();

            return success;

        }

        protected internal bool EditStudent(Student student)
        {
            bool success = false;
            if (student.Id != default)
            {
                context.Entry(student).State = EntityState.Modified;
                success = true;
            }

            context.SaveChanges();

            return success;
        }

        protected internal Student GetOneStudentBy(int? id)
        {
            Student student = new Student();
            student = context.Students.Include(s => s.Group).FirstOrDefault(m => m.Id == id);
            return student;
        }

        protected internal Student GetOneStudentForDeleteBy(int? id)
        {
            Student student = new Student();
            student = context.Students.Include(s => s.Group).FirstOrDefault(m => m.Id == id);
            return student;
        }

        protected internal IQueryable<Student> GetStudentsByGroup(int? id)
        {
            return context.Students.Include(s => s.Group).Where(g => g.GroupId == id);
        }

        protected internal bool RemoveStudentBy(int id)
        {
            bool succces = false;
            try
            {
                context.Students.Remove(new Student() { Id = id });
                context.SaveChanges();
                succces = true;
            }
            catch
            {
                succces = false;
            }

            return succces;
        }

    }
}
