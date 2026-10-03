using LMS.BLL.ModelVM.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.Instructor
{
    public class RegisterInstructorVM : RegisterVM
    {
        [Required(ErrorMessage = "Professional bio is required.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Bio must be between 10 and 1000 characters")]
        public string Bio { get; set; } = string.Empty;

        [Required(ErrorMessage = "Academic degree is required (e.g. Bachelor, Master).")]
        [MaxLength(100, ErrorMessage = "Academic degree cannot exceed 100 characters")]
        public string AcademicDegree { get; set; } = string.Empty;

        [Required(ErrorMessage = "Years of experience is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Years of experience must be a positive number.")]
        public int YearsOfExperience { get; set; }

        [Required(ErrorMessage = "Job title is required (e.g. Senior DotNet Developer).")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;
    }
}
