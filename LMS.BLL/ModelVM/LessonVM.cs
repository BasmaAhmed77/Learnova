using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Entities;

namespace LMS.BLL.ModelVM
{
    public class LessonVM
    {
        public int LessonId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? ContentType { get; set; }
        public string url { get; set; }
        public string? videoID { get; set; }
        public int? Chapter { get; set; }
        [ForeignKey("Module")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public Quiz? Quiz { get; set; }
    }
}
