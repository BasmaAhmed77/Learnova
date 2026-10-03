using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Instructor
    {
        public int InstructorId { get; private set; }
        public string Bio { get; private set; }
        public string AcademicDegree { get; private set; }
        public int YearsOfExperience { get; private set; }
        public string Title { get; private set; }
        public bool IsVerified { get; private set; } = false;
        public string UserId { get; private set; }
        public UserAccount UserAccount { get; set; }
        public List<Course> Courses { get; private set; } = new List<Course>();
        private Instructor() { }
        public Instructor(string userId, string bio, string degree, int experience, string title)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.");

            if (string.IsNullOrWhiteSpace(bio))
                throw new ArgumentException("Bio is required.");

            if (string.IsNullOrWhiteSpace(degree))
                throw new ArgumentException("Academic Degree is required.");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            if (experience < 0)
                throw new ArgumentOutOfRangeException(nameof(experience), "Years of experience cannot be negative.");
            UserId = userId;
            Bio = bio;
            AcademicDegree = degree;
            YearsOfExperience = experience;
            Title = title;
        }
        public void UpdateProfessionalBio(string bio)
        {
            if (string.IsNullOrWhiteSpace(bio))
                throw new ArgumentException("Bio cannot be empty.");
            Bio = bio;
        }

        public void UpdateAcademicDegree(string degree)
        {
            if (string.IsNullOrWhiteSpace(degree))
                throw new ArgumentException("Academic degree is required.");

            AcademicDegree = degree;
        }

        public void UpdateJobTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            Title = title;
        }

        public void UpdateExperience(int experience)
        {
            if (experience < 0)
                throw new ArgumentOutOfRangeException(nameof(experience), "Years of experience cannot be negative.");
            YearsOfExperience = experience;
        }
        public void Verify()
        {
            IsVerified = true;
        }
    }
}
