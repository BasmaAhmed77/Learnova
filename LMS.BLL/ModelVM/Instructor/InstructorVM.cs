using LMS.BLL.ModelVM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.Instructor
{
    public record InstructorVM : UserAccountVM
    {
        public int InstructorId { get; set; }
        public string Bio { get; set; }
        public bool IsVerified { get; set; }
        public string AcademicDegree { get; set; }
        public int YearsOfExperience { get; set; }
        public string Title { get; set; }
    }
}
