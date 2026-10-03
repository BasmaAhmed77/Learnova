using AutoMapper;
using LMS.BLL.ModelVM.Student;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Repo.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Implementation
{
    public sealed class StudentService :IStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public StudentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentVM>> GetAllAsync(CancellationToken ct = default)
        {
            var students = await _uow.Students.GetAllStudentsAsync(ct);
            return _mapper.Map<IEnumerable<StudentVM>>(students);
        }

        public async Task<StudentVM?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var student = await _uow.Students.GetByIdAsync(id, ct);

            if (student == null || student.UserAccount == null) 
                return null;

            return _mapper.Map<StudentVM>(student);
        }

        public async Task<StudentVM?> GetByUserIdAsync(string userId, CancellationToken ct = default)
        {
            var student = await _uow.Students.GetByUserIdAsync(userId, ct);

            if (student == null || student.UserAccount == null) 
                return null;

            return _mapper.Map<StudentVM>(student);
        }

        public async Task<List<int>> GetEnrolledCourseIdsAsync(string userId, CancellationToken ct = default)
        {
            var student = await _uow.Students.GetByUserIdAsync(userId, ct);
            if (student == null) 
                return new List<int>();
            return await _uow.Students.GetEnrolledIdsByStudentIdAsync(student.StudentId, ct);
        }
    }
}
