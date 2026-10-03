using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Database;
using LMS.DAL.Entities;
using LMS.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace LMS.DAL.Repo.Implementation
{
    public class LessonRepo : ILessonRepo
    {
            private readonly LMSDBContext _dbContext;

            public LessonRepo(LMSDBContext _context)
            {
                _dbContext = _context;
            }

            public async Task<Lesson> createLessonByCourseIdAsync(int CourseId) //create new lesson
            {
                Course course = _dbContext.Courses.Find(CourseId);
                Lesson lesson = new Lesson();
                lesson.Course = course;
                lesson.CourseId = course.CourseId;
                return lesson;
            }

            public async Task<Lesson> getLessonByIdAsync(int LessonID)
            {
                var lesson = await _dbContext.Lessons.FindAsync(LessonID);
                return lesson;
            }
            public async Task deleteLessonByIdAsync(int LessonId)
            {
                Lesson lesson = await _dbContext.Lessons.FindAsync(LessonId);
                _dbContext.Lessons.Remove(lesson);
                await _dbContext.SaveChangesAsync();
            }
    
            public async Task addLessonAsync(Lesson lesson) //add lesson to db
            {
                _dbContext.Lessons.Add(lesson);
                await _dbContext.SaveChangesAsync();
            }
            public async Task updateLessonAsync(Lesson lesson)
            {
                _dbContext.Lessons.Update(lesson);
                await _dbContext.SaveChangesAsync();
            }
        
    }


}

