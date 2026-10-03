using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.BLL.ModelVM
{    public class SubmissionVM
        {
            public int Id { get; set; }

            [Required]
            public int AssignmentId { get; set; }
            public string AssignmentTitle { get; set; }  // for display
            public DateTime DueDate { get; set; }         // for display

            public int StudentId { get; set; }
            public string StudentName { get; set; }       // for display

            // Student fills one or both
            public IFormFile File { get; set; }           // file upload
            public string TextContent { get; set; }       // text submission

            public DateTime SubmittedAt { get; set; }
            public bool IsGraded { get; set; }

            // Shown after grading (FR-14)
            public double? Grade { get; set; }
            public string Feedback { get; set; }
        }
    
}
