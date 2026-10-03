using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.BLL.ModelVM
{
       public class GradeVM
        {
            public int SubmissionId { get; set; }
            public string AssignmentTitle { get; set; }  // for display
            public string StudentName { get; set; }       // for display
            public DateTime SubmittedAt { get; set; }

            // What student submitted
            public string FilePath { get; set; }
            public string TextContent { get; set; }

            // Instructor fills these
            [Required]
            [Range(0, 100)]
            public double Grade { get; set; }

            [Required]
            public string Feedback { get; set; }
        
    }
}
