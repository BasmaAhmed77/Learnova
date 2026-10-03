using AutoMapper;
using LMS.BLL.ModelVM.Instructor;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Repo.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Implementation
{
    public sealed class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public InstructorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InstructorVM>> GetAllAsync(CancellationToken ct = default)
        {
            var instructors = await _uow.Instructors.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<InstructorVM>>(instructors);
        }

        public async Task<InstructorVM?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var instructor = await _uow.Instructors.GetByIdAsync(id, ct);
            return _mapper.Map<InstructorVM>(instructor);
        }

        public async Task<InstructorVM?> GetByUserIdAsync(string userId, CancellationToken ct = default)
        {
            var instructor = await _uow.Instructors.GetByUserIdAsync(userId, ct);
            return _mapper.Map<InstructorVM>(instructor);
        }
        public async Task<bool> UpdateBioAsync(string userId, UpdateInstructorBioVM model, CancellationToken ct = default)
        {
            var instructor = await _uow.Instructors.GetByUserIdAsync(userId, ct);
            if (instructor == null) 
                return false;
            if (instructor.Bio == model.Bio) 
                return true;
            await _uow.Instructors.UpdateBioAsync(instructor.InstructorId, model.Bio, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateExperienceAsync(string userId, UpdateInstructorYearsOfExperienceVM model, CancellationToken ct = default)
        {
            var instructor = await _uow.Instructors.GetByUserIdAsync(userId, ct);
            if (instructor == null) 
                return false;
            if (instructor.YearsOfExperience == model.YearsOfExperience) 
                return true;
            await _uow.Instructors.UpdateExperienceAsync(instructor.InstructorId, model.YearsOfExperience, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateAcademicDegreeAsync(string userId, UpdateInstructorAcademicDegreeVM model, CancellationToken ct = default)
        {
            var instructor = await _uow.Instructors.GetByUserIdAsync(userId, ct);
            if (instructor == null)
                return false;
            if (instructor.AcademicDegree == model.AcademicDegree) 
                return true;
            await _uow.Instructors.UpdateAcademicDegreeAsync(instructor.InstructorId, model.AcademicDegree, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateTitleAsync(string userId, UpdateInstructorTitleVM model, CancellationToken ct = default)
        {
            var instructor = await _uow.Instructors.GetByUserIdAsync(userId, ct);
            if (instructor == null) 
                return false;
            if (instructor.Title == model.Title) 
                return true;
            await _uow.Instructors.UpdateTitleAsync(instructor.InstructorId, model.Title, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> VerifyInstructorAsync(string userId, CancellationToken ct = default)
        {
            var instructor = await _uow.Instructors.GetByUserIdAsync(userId, ct);
            if (instructor == null) 
                return false;
            if (instructor.IsVerified) 
                return true;
            await _uow.Instructors.VerifyInstructorAsync(instructor.InstructorId, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
