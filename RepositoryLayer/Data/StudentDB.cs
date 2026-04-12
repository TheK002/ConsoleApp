using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public static class StudentDB<T>
    {
        public static List<T> students;

        static StudentDB()
        {
            students = new List<T>();
        }
    }
}
