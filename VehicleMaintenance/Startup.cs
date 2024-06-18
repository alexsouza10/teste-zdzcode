using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace VehicleMaintenance
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IMaintenanceRecordRepository, MaintenanceRecordRepository>();
            services.AddScoped<IMaintenanceServiceRepository, MaintenanceServiceRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();

            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IMaintenanceRecordService, MaintenanceRecordService>();
            services.AddScoped<IMaintenanceServiceService, MaintenanceServiceService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
