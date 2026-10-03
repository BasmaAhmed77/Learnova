using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.Instructor
{
    public record UpdateInstructorBioVM(
        [Required(ErrorMessage = "Professional bio is required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Bio must be between 10 and 1000 characters")]
        string Bio
    );
}
