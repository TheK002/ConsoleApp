using DomainLayer.Entites;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories.Implementations
{
    public class CourseGroupRepository : ICourseGroupRepository<CourseGroup>
    {
        public void Create(CourseGroup data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            CourseGroupDB<CourseGroup>.courseGroups.Add(data);
        }

        public void Update(CourseGroup data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var existing = GetById(data.Id);
            if (existing != null)
            {
                existing.Name = data.Name;
                existing.Teacher = data.Teacher;
                existing.Room = data.Room;
            }
        }

        public void Delete(int id)
        {
            var courseGroup = GetById(id);
            if (courseGroup != null)
            {
                CourseGroupDB<CourseGroup>.courseGroups.Remove(courseGroup);
            }
        }

        public CourseGroup GetById(int id)
        {
            return CourseGroupDB<CourseGroup>.courseGroups.FirstOrDefault(cg => cg.Id == id);
        }

        public CourseGroup Get(Predicate<CourseGroup> predicate)
        {
            return CourseGroupDB<CourseGroup>.courseGroups.FirstOrDefault(cg => predicate(cg));
        }

        public List<CourseGroup> GetAll()
        {
            return CourseGroupDB<CourseGroup>.courseGroups.ToList();
        }

        public List<CourseGroup> GetAll(Predicate<CourseGroup> predicate)
        {
            return CourseGroupDB<CourseGroup>.courseGroups.Where(cg => predicate(cg)).ToList();
        }
    }
}