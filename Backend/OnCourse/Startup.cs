using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnCourse.Data;
using OnCourse.Extensions;
using OnCourse.Repositories;
using OnCourse.Repositories.Interfaces;
using OnCourse.Settings;

namespace OnCourse
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OnCourseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            // Is not being used at the moment
            services.AddCorsExtension();

            var jwtSection = Configuration.GetSection("JwtConfiguration");
            var jwtConfiguration = jwtSection.Get<JwtConfiguration>();

            services.Configure<JwtConfiguration>(jwtSection);

            services.AddAuthenticationExtension(jwtConfiguration);

            // Is not being used at the moment
            services.AddAuthorizationExtension();

            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddControllers();

            services.AddSwaggerGenExtension();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnCourse v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Angular");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
