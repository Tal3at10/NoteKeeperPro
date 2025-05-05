using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Infrastructure.Presistance.Data;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Users;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.NotesInfo;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Collaborators;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Tags;

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
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            builder.Services.AddScoped<ITagRepository,TagRepository>();
            builder.Services.AddScoped<INoteInfoRepository, NoteInfoRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
