using AutoMapper;
using Hangfire.Server;
using LMS.BLL.ModelVM.Instructor;
using LMS.BLL.ModelVM.Student;
using LMS.BLL.ModelVM.User;
using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.ModelVM;

namespace LMS.BLL.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Instructor, InstructorVM>()
                .ForMember(d => d.Fname, o => o.MapFrom(s => s.UserAccount.Fname))
                .ForMember(d => d.Lname, o => o.MapFrom(s => s.UserAccount.Lname))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.UserAccount.Email))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.UserAccount.PhoneNumber))
                .ForMember(d => d.ProfilePicture, o => o.MapFrom(s => s.UserAccount.ProfilePicture));

            CreateMap<Student, StudentVM>()
                .ForMember(d => d.Fname, o => o.MapFrom(s => s.UserAccount.Fname))
                .ForMember(d => d.Lname, o => o.MapFrom(s => s.UserAccount.Lname))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.UserAccount.Email))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.UserAccount.PhoneNumber))
                .ForMember(d => d.ProfilePicture, o => o.MapFrom(s => s.UserAccount.ProfilePicture));

            CreateMap<UserAccount, UserAccountVM>();
            CreateMap<Course,CourseVM>().ReverseMap();
            CreateMap<Lesson,LessonVM>().ReverseMap();
        }
    }
}
