using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public class CourseGroupDB<T>
    {
        public static List<T> courseGroups;

        static CourseGroupDB()
        {
            courseGroups = new List<T>();
        }
    }
}
