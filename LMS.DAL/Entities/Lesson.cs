using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LMS.DAL.Entities
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? ContentType { get; set; }
        public string url { get; set; }
        public string? videoID { get; set; }
        public int? Chapter { get; set; }
        //public IFormFile MaterialFile { get; set; }
        [ForeignKey("Module")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public Quiz? Quiz { get; set; }
    }
}

