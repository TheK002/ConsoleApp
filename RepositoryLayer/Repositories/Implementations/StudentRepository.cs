using DomainLayer.Entites;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository<Student>
    {
        public void Create(Student data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            StudentDB<Student>.students.Add(data);
        }

        public void Update(Student data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var existing = GetById(data.Id);
            if (existing != null)
            {
                existing.Name = data.Name;
                existing.Surname = data.Surname;
                existing.Age = data.Age;
                existing.CourseGroup = data.CourseGroup;
            }
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                StudentDB<Student>.students.Remove(student);
            }
        }

        public Student GetById(int id)
        {
            return StudentDB<Student>.students.FirstOrDefault(s => s.Id == id);
        }

        public Student Get(Predicate<Student> predicate)
        {
            return StudentDB<Student>.students.FirstOrDefault(s => predicate(s));
        }

        public List<Student> GetAll()
        {
            return StudentDB<Student>.students.ToList();
        }

        public List<Student> GetAll(Predicate<Student> predicate)
        {
            return StudentDB<Student>.students.Where(s => predicate(s)).ToList();
        }
    }
}