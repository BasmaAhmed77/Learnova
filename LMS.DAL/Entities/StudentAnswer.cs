using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class StudentAnswer
    {
        [ForeignKey("StudentId, QuizId")]
        public int StudentId { get; set; }
        public int QuizId { get; set; }
        public StudentQuizSubmission Submission { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        [ForeignKey("ChosenOption")]
        public int ChosenOptionId { get; set; }
        public Option ChosenOption { get; set; }
    }
}
