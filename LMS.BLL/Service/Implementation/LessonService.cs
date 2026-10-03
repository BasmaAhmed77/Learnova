using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Database;
using LMS.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using LMS.BLL.ModelVM;
using AutoMapper;



namespace LMS.BLL.Service.Implementation
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepo _LessonRepo;
        private readonly IMapper _mapper;
        //private readonly ICourseRepo _CourseRepo;

        public LessonService(ILessonRepo lessonRepo, IMapper mapper)
        {
            _mapper = mapper;
            _LessonRepo = lessonRepo;
            //_CourseRepo= courseRepo;
        }

        public async Task<LessonVM> createLessonByCourseID(int CourseId) //create new lesson
        {

            var lesson = await _LessonRepo.createLessonByCourseIdAsync(CourseId);
            return _mapper.Map<LessonVM>(lesson);
        }

        public async Task<LessonVM> getLessonById(int LessonID)
        {
            var lesson = await _LessonRepo.getLessonByIdAsync(LessonID);
            return _mapper.Map<LessonVM>(lesson);
        }

        public async Task deleteLessonById(int LessonId, int CourseId)
        {
            await _LessonRepo.deleteLessonByIdAsync(LessonId);
        }

        public async Task addLesson(LessonVM lessonVM, string id) //add lesson to db
        {
            lessonVM.videoID = id;
            var lesson = _mapper.Map<Lesson>(lessonVM);
            await _LessonRepo.addLessonAsync(lesson);

            //Course course = await _CourseRepo.getCourseByIdAsync(lesson.CourseId);
            //course.Lessons.Add(lesson);
        }
        public async Task updateLesson(LessonVM lessonVM, string id)
        {
            lessonVM.videoID = id;
            var lesson = _mapper.Map<Lesson>(lessonVM);
            await _LessonRepo.updateLessonAsync(lesson);
        }



        public async Task<string> getVideoID(string url)
        {
            string videoID = "";
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            if (url.Contains("youtu.be/"))
            {
                videoID = url.Split("youtu.be/")[1].Split("?")[0];
            }
            else if (url.Contains("v="))
            {
                videoID = url.Split("v=")[1].Split("&")[0];
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCyRA-LrbUTB2shgp2c84yqiz7H8iLy958",
                ApplicationName = "LMSProject!!!"
            });

            var request = youtubeService.Videos.List("id");
            request.Id = videoID;
            var response = await request.ExecuteAsync();

            if (response.Items == null || response.Items.Count == 0)
            {
                return null;
            }
            return videoID;
        }
    }
}

