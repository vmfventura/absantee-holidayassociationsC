using Application.Services;
using DataModel.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddDbContext<AbsanteeContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionString"]);
                });
            // Register ProjectService
            services.AddScoped<HolidayService>();
            services.AddScoped<AssociationService>();
            services.AddScoped<HolidaysAssociationsService>();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}