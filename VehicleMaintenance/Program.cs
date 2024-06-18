using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Testar conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    using (var connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();
        Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao conectar com o banco de dados: {ex.Message}");
}

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vehicle Maintenance API", Version = "v1" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IMaintenanceServiceRepository, MaintenanceServiceRepository>();
builder.Services.AddScoped<IMaintenanceRecordRepository, MaintenanceRecordRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<IMaintenanceServiceService, MaintenanceServiceService>();
builder.Services.AddScoped<IMaintenanceRecordService, MaintenanceRecordService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle Maintenance API V1");
    });
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();