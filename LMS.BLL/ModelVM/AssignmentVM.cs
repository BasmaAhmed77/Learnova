using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace LMS.BLL.ModelVM
{
    public class AssignmentVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public string SubmissionRules { get; set; }

        [Required]
        public int CourseId { get; set; }
        public string CourseName { get; set; }  // for display

        public int InstructorId { get; set; }

        // For dropdown in view
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
