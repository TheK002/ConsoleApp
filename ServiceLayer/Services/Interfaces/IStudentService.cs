using DomainLayer.Entites;
using RepositoryLayer.Data;

namespace ServiceLayer.Interfaces
{
    public interface IStudentService
    {
        Student Create(Student student);
        Student Update(int id, Student student);
        void Delete(int id);
        Student GetStudent(int id);
        List<Student> GetAll();
        List<Student> GetByAge(int age);
        List<Student> GetByGroupId(int groupId);
        List<Student> Search(string text);
    }

}