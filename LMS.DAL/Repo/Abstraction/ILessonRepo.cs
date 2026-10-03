using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace LMS.DAL.Repo.Abstraction
{
    public interface ILessonRepo
    {
        public Task<Lesson> createLessonByCourseIdAsync(int CourseId); //create new lesson

        public Task<Lesson> getLessonByIdAsync(int LessonID);
        public Task deleteLessonByIdAsync(int LessonId);
        public Task addLessonAsync(Lesson lesson); //add lesson to db
        public Task updateLessonAsync(Lesson lesson);

    }
}
