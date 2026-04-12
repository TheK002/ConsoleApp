using DomainLayer.Entites;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private StudentRepository _studentRepository;
        private int _nextId;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
            _nextId = _studentRepository.GetAll().Count + 1;
        }

        public Student Create(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Student name cannot be empty");

            if (string.IsNullOrWhiteSpace(student.Surname))
                throw new ArgumentException("Student surname cannot be empty");

            if (student.Age < 18 || student.Age > 100)
                throw new ArgumentException("Student age must be between 18 and 100");

            student.Id = _nextId++;
            _studentRepository.Create(student);
            return student;
        }

        public Student Update(int id, Student student)
        {
            var existing = _studentRepository.GetById(id);
            if (existing == null)
                throw new NotFoundException($"Student with ID {id} not found");

            if (!string.IsNullOrWhiteSpace(student.Name))
                existing.Name = student.Name;

            if (!string.IsNullOrWhiteSpace(student.Surname))
                existing.Surname = student.Surname;

            if (student.Age > 0)
                existing.Age = student.Age;

            if (student.CourseGroup != null)
                existing.CourseGroup = student.CourseGroup;

            _studentRepository.Update(existing);
            return existing;
        }

        public void Delete(int id)
        {
            var existing = _studentRepository.GetById(id);
            if (existing == null)
                throw new NotFoundException($"Student with ID {id} not found");

            _studentRepository.Delete(id);
        }

        public Student GetStudent(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found");

            return student;
        }

        public List<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public List<Student> GetByAge(int age)
        {
            return _studentRepository.GetAll(s => s.Age == age);
        }

        public List<Student> GetByGroupId(int groupId)
        {
            return _studentRepository.GetAll(s => s.CourseGroup != null && s.CourseGroup.Id == groupId);
        }

        public List<Student> Search(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return _studentRepository.GetAll();

            return _studentRepository.GetAll(s =>
                s.Name.ToLower().Contains(text.ToLower()) ||
                s.Surname.ToLower().Contains(text.ToLower()));
        }
    }
}