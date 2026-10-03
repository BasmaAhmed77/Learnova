using LMS.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Database
{
    public class LMSDBContext : IdentityDbContext<UserAccount> 
    {
        public LMSDBContext() : base()
        {
        }
        public LMSDBContext(DbContextOptions<LMSDBContext> options) : base(options)
        {
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.Property(u => u.Fname).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Lname).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Gender).IsRequired();
                entity.Property(u => u.DOB).IsRequired();
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasKey(i => i.InstructorId);
                entity.Property(i => i.Bio).IsRequired();
                entity.Property(i => i.AcademicDegree).IsRequired().HasMaxLength(100);
                entity.Property(i => i.Title).IsRequired().HasMaxLength(100);
                entity.Property(i => i.YearsOfExperience).IsRequired();

                entity.HasOne(i => i.UserAccount)
                      .WithOne(u => u.Instructor)
                      .HasForeignKey<Instructor>(i => i.UserId)
                      .IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);
                entity.HasOne(s => s.UserAccount)
                      .WithOne(u => u.Student)
                      .HasForeignKey<Student>(s => s.UserId)
                      .IsRequired();
            });

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentQuizSubmission>()
                .HasKey(sqs => new { sqs.StudentId, sqs.QuizId });

            modelBuilder.Entity<StudentQuizSubmission>()
                .HasOne(sqs => sqs.Student)
                .WithMany(s => s.Submissions)
                .HasForeignKey(sqs => sqs.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentQuizSubmission>()
                .HasOne(sqs => sqs.Quiz)
                .WithMany(q => q.Submissions)
                .HasForeignKey(sqs => sqs.QuizId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAnswer>()
                .HasKey(sa => new { sa.StudentId, sa.QuizId, sa.QuestionId });

            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Submission)
                .WithMany(sqs => sqs.StudentAnswers)
                .HasForeignKey(sa => new { sa.StudentId, sa.QuizId })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Question)
                .WithMany() 
                .HasForeignKey(sa => sa.QuestionId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.ChosenOption)
                .WithMany()
                .HasForeignKey(sa => sa.ChosenOptionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Lesson)
                .WithOne(l => l.Quiz)
                .HasForeignKey<Quiz>(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
            

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


           
            modelBuilder.Entity<Assignment>()
       .HasOne(a => a.Instructor)
       .WithMany()
       .HasForeignKey(a => a.InstructorId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany()
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<StudentQuizSubmission> StudentQuizSubmissions { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

    }
}