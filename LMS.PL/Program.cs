using LMS.BLL.AutoMapper;
using LMS.BLL.Helper;
using LMS.BLL.ModelVM;
using LMS.BLL.Service.Abstraction;
using LMS.BLL.Service.Implementation;
using LMS.DAL.Database;
using LMS.DAL.Entities;
using LMS.DAL.Repo.Abstraction;
using LMS.DAL.Repo.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));
            var connectionString = builder.Configuration.GetConnectionString("Connection");
            builder.Services.AddDbContext<LMSDBContext>(options =>
                options.UseSqlServer(connectionString));
           
            builder.Services.AddDbContext<LMSDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Connection"),
        b => b.MigrationsAssembly("LMS.PL") 
    )
);

            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<UserAccount, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                })
            .AddEntityFrameworkStores<LMSDBContext>()
            .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(14); 
                options.SlidingExpiration = true;
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<Upload>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            builder.Services.AddScoped<IAssignmentRepo, AssignmentRepo>();
            builder.Services.AddScoped<IAssignmentService, AssignmentService>();

            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ILessonService, LessonService>();
            builder.Services.AddScoped<ICourseRepo, CourseRepo>();
            builder.Services.AddScoped<ILessonRepo, LessonRepo>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            //builder.Services.AddAutoMapper(typeof(DomainProfile));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Seed roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "Admin", "Student", "Instructor" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }

            app.Run();
        }
    }
}