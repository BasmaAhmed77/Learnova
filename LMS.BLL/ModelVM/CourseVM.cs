using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using LMS.DAL.Database;

namespace LMS.BLL.ModelVM
{
    public class CourseVM
    {
            public int CourseId { get; set; }

            [MaxLength(50)]
            public string Title { get; set; }

            [MaxLength(500)]
            public string Description { get; set; }

            public string Category { get; set; }

          
            public decimal Price { get; set; }

            public string Language { get; set; }

            public string Level { get; set; }

            public string isFree { get; set; }

            public string? imageURL { get; set; }

            [ForeignKey("Instructor")]
            public int InstructorId { get; set; }
        public LMS.DAL.Entities.Instructor? Instructor { get; set; }
        public List<Lesson>? Lessons { get; set; }
            public List<StudentCourse>? StudentCourses { get; set; }
        
    }

}

