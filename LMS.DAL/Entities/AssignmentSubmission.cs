using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
   
        public class AssignmentSubmission
        {
            public int Id { get; set; }
            public int AssignmentId { get; set; }
            public int StudentId { get; set; }
            public string FilePath { get; set; }      // for file upload
            public string TextContent { get; set; }   // for text submission
            public DateTime SubmittedAt { get; set; }
            public double? Grade { get; set; }        // FR-13
            public string Feedback { get; set; }      // FR-13
            public bool IsGraded { get; set; }

            // Navigation
            public Assignment Assignment { get; set; }
            public Student Student { get; set; }
        }
    
}
