using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LMS.DAL.Entities;
using LMS.BLL.ModelVM;

namespace LMS.BLL.Service.Abstraction
{
    public interface ILessonService
    {
        public Task<LessonVM> createLessonByCourseID(int CourseId); //create new lesson

        public Task<LessonVM> getLessonById(int LessonID);

        public Task deleteLessonById(int LessonId, int CourseId);

        public Task<string> getVideoID(string url);

        public Task addLesson(LessonVM lesson, string id); //add lesson to db

        public Task updateLesson(LessonVM lesson, string id);
    }
}
