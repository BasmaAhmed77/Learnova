using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.Instructor
{
    public record UpdateInstructorAcademicDegreeVM(
        [Required(ErrorMessage = "Academic degree is required (e.g. Bachelor, Master).")]
        [MaxLength(100, ErrorMessage = "Academic degree cannot exceed 100 characters")]
        string AcademicDegree
    );
}
