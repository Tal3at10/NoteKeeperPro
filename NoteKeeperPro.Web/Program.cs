using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Infrastructure.Presistance.Data;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.NotesInfo;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Collaborators;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Tags;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using NoteKeeperPro.Application.Common.Services.EmailSettings;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.ApplicationUsers;
using NoteKeeperPro.Domain.Entities.ApplicationUsers;

namespace NoteKeeperPro.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>((options) =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<INoteInfoRepository, NoteInfoRepository>();
            builder.Services.AddScoped<IEmailSettings, EmailSettings>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Authentication and Authorization configuration
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(Options =>
                {
                    Options.LoginPath = "/Account/Login"; // Path for login
                    Options.AccessDeniedPath = "/Home/Error"; // Path for access denied
                    Options.LogoutPath = "/Account/Logout"; // Path for logout
                    Options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter; // Ensures user returns to the original page after login
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); // Ensure authentication is before authorization
            app.UseAuthorization();

            // Default route - ensure it routes to MainPage/Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/"); // This routes to MainPage/Index by default

            app.Run();

        }
    }
}
