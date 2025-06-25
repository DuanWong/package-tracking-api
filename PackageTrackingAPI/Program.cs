using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.BLL;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework Core
builder.Services.AddDbContext<PackageTrackingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<TrackingEventService>();
builder.Services.AddScoped<AlertService>();

// Register Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PackageRepository>();
builder.Services.AddScoped<TrackingEventRepository>();
builder.Services.AddScoped<AlertRepository>();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();