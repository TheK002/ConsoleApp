using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories.Interfaces
{
    public interface IStudentRepository<T>
    {
        void Create(T data);
        void Update(T data);
        void Delete(int id);
        T GetById(int id);
        T Get(Predicate<T> predicate);
        List<T> GetAll();
        List<T> GetAll(Predicate<T> predicate);
    }
}
