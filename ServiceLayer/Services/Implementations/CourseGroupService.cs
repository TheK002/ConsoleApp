using DomainLayer.Entites;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services.Implementations
{
    public class CourseGroupService : ICourseGroupService
    {
        private CourseGroupRepository _courseGroupRepository;
        private int _nextId;

        public CourseGroupService()
        {
            _courseGroupRepository = new CourseGroupRepository();
            _nextId = _courseGroupRepository.GetAll().Count + 1;
        }

        public CourseGroup Create(CourseGroup courseGroup)
        {
            if (string.IsNullOrWhiteSpace(courseGroup.Name))
                throw new ArgumentException("Course group name cannot be empty");

            if (string.IsNullOrWhiteSpace(courseGroup.Teacher))
                throw new ArgumentException("Teacher name cannot be empty");

            if (courseGroup.Room <= 0)
                throw new ArgumentException("Room number must be positive");

            var existing = _courseGroupRepository.Get(cg => cg.Name.ToLower() == courseGroup.Name.ToLower());
            if (existing != null)
                throw new InvalidOperationException($"Course group with name '{courseGroup.Name}' already exists");

            courseGroup.Id = _nextId++;
            _courseGroupRepository.Create(courseGroup);
            return courseGroup;
        }

        public CourseGroup Update(int id, CourseGroup courseGroup)
        {
            var existing = _courseGroupRepository.GetById(id);
            if (existing == null)
                throw new NotFoundException($"Course group with ID {id} not found");

            if (!string.IsNullOrWhiteSpace(courseGroup.Name))
                existing.Name = courseGroup.Name;

            if (!string.IsNullOrWhiteSpace(courseGroup.Teacher))
                existing.Teacher = courseGroup.Teacher;

            if (courseGroup.Room > 0)
                existing.Room = courseGroup.Room;

            _courseGroupRepository.Update(existing);
            return existing;
        }

        public void Delete(int id)
        {
            var existing = _courseGroupRepository.GetById(id);
            if (existing == null)
                throw new NotFoundException($"Course group with ID {id} not found");

            _courseGroupRepository.Delete(id);
        }

        public CourseGroup GetCourseGroup(int id)
        {
            var courseGroup = _courseGroupRepository.GetById(id);
            if (courseGroup == null)
                throw new NotFoundException($"Course group with ID {id} not found");

            return courseGroup;
        }

        public List<CourseGroup> GetAll()
        {
            return _courseGroupRepository.GetAll();
        }

        public List<CourseGroup> Search(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return _courseGroupRepository.GetAll();

            return _courseGroupRepository.GetAll(cg =>
                cg.Name.ToLower().Contains(text.ToLower()) ||
                cg.Teacher.ToLower().Contains(text.ToLower()));
        }

        public List<CourseGroup> GetByTeacher(string teacherName)
        {
            if (string.IsNullOrWhiteSpace(teacherName))
                return new List<CourseGroup>();

            return _courseGroupRepository.GetAll(cg =>
                cg.Teacher.ToLower().Contains(teacherName.ToLower()));
        }

        public List<CourseGroup> GetByRoom(int room)
        {
            return _courseGroupRepository.GetAll(cg => cg.Room == room);
        }
    }
}