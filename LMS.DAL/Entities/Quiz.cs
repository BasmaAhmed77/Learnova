using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        public string? Title { get; set; }

        public int? TotalMarks { get; set; }

        public int? PassingScore { get; set; }

        [ForeignKey("Lesson")] 
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }
        public List<Question>? Questions { get; set; }
        public List<StudentQuizSubmission>? Submissions { get; set; }

    }
}
