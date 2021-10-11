using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using University.Models;
using University.ViewModels;

namespace University.Serviñes
{
    public class StudentService
    {
        private readonly UniversityContext _context;
        private readonly IMapper _mapper;

        public StudentService(UniversityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        protected internal bool AddStudent(StudentDto studentDTO)
        {
            bool success = false;

            var student = _mapper.Map<Student>(studentDTO);

            if (student.Id == default)
            {
                _context.Entry(student).State = EntityState.Added;
                _context.SaveChanges();
                success = true;
            }

            return success;

        }

        protected internal bool EditStudent(StudentDto studentDTO)
        {
            bool success = false;

            var student = _mapper.Map<Student>(studentDTO);

            if (student.Id != default)
            {
                _context.Entry(student).State = EntityState.Modified;
                _context.SaveChanges();
                success = true;
            }
            
            return success;
        }

        protected internal Student GetOneStudentOrDefaultBy(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return _context.Students.Include(s => s.Group).FirstOrDefault(m => m.Id == id);
        }
                
        protected internal Student GetOneStudentForDeleteBy(int? id)
                => _context.Students.Include(s => s.Group).FirstOrDefault(m => m.Id == id);
           
        protected internal IQueryable<Student> GetStudentsByGroup(int? id)
                =>_context.Students.Include(s => s.Group).Where(g => g.GroupId == id);
      
        protected internal bool RemoveStudentBy(int id)
        {
            bool succces = false;

            try
            {
                _context.Students.Remove(new Student() { Id = id });
                _context.SaveChanges();
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
