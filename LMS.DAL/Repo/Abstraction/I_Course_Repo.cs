using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Abstraction
{
   
        public interface I_Course_Repo {
        IEnumerable<Course> GetAll();
        Course Get_by_ID(int id);
        void add_course(Course course);
        void remove_course(Course course);
        void update_course(Course course);

        }
    
}
