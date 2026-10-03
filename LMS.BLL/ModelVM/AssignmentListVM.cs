using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace LMS.BLL.ModelVM
{
        public class AssignmentListVM
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string CourseName { get; set; }
            public DateTime DueDate { get; set; }
            public int TotalSubmissions { get; set; }   // instructor sees this
            public bool IsSubmitted { get; set; }        // student sees this
            public bool IsGraded { get; set; }
            public double? Grade { get; set; }
        }
    
}
