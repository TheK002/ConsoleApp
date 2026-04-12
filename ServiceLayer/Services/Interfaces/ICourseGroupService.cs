using DomainLayer.Entites;

namespace ServiceLayer.Interfaces
{
    public interface ICourseGroupService
    {
        CourseGroup Create(CourseGroup courseGroup);
        CourseGroup Update(int id, CourseGroup courseGroup);
        void Delete(int id);
        CourseGroup GetCourseGroup(int id);
        List<CourseGroup> GetAll();
        List<CourseGroup> Search(string text);
        List<CourseGroup> GetByTeacher(string teacherName);
        List<CourseGroup> GetByRoom(int room);
    }
}