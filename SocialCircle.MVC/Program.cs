using SocialCircle.BLL;
using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();


            builder.Services.AddDbContext<SocialCircleContext>();

            
            builder.Services.AddTransient<UserRepository>();
            builder.Services.AddTransient<FollowRepository>();

            builder.Services.AddTransient<PostRepository>();
            builder.Services.AddTransient<CommentRepository>();

            builder.Services.AddTransient<UserService>();
            builder.Services.AddTransient<FollowService>();

            builder.Services.AddTransient<PostService>();
            builder.Services.AddTransient<CommentService>();

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

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
