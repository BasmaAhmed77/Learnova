using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Student
    {
        public int StudentId { get; private set; }
        public string UserId { get; private set; }
        public UserAccount UserAccount { get; set; }
        public List<StudentCourse> StudentCourses { get; private set; } = new();
        public List<StudentQuizSubmission> Submissions { get; private set; } = new();
        private Student() { }
        public Student(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required");
            UserId = userId;
        }
    }
}
