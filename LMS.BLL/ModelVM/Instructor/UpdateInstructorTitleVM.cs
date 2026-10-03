using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.Instructor
{
    public record UpdateInstructorTitleVM(
        [Required(ErrorMessage = "Job title is required (e.g. Senior DotNet Developer).")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        string Title
    );
}
