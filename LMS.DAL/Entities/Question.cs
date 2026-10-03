using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string Header { get; set; }
        public int Mark { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public List<Option> Options { get; set; }
    }
}
