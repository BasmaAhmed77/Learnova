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
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        //[Required (ErrorMessage ="Title is required")]
        [MaxLength(50)]
        public string Title { get; set; }

        //[Required(ErrorMessage = "Description is required")]
        [MaxLength(500)]
        public string Description { get; set; }

        //[Required]
        public string Category { get; set; }

        //[Required]
        //[Range(100,10000, ErrorMessage = "Price must be in [100$ , 10000$]")]
        public decimal Price { get; set; }

        //[Required]
        public string Language { get; set; }

        //[Required]
        public string Level { get; set; }

        //[Required]
        public string isFree { get; set; }

        public string? imageURL { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
