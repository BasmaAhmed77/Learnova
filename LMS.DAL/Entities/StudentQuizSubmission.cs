using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class StudentQuizSubmission
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int TotalMark { get; set; }

        public DateTime SubmissionDate { get; set; }


        public List<StudentAnswer> StudentAnswers { get; set; }
    }
}
