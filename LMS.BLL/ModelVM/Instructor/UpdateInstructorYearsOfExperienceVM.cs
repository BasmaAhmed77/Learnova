using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.Instructor
{
    public record UpdateInstructorYearsOfExperienceVM(
        [Required(ErrorMessage = "Years of experience is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Years of experience must be a positive number.")]
        int YearsOfExperience
    );
}
