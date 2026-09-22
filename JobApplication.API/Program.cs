using JobApplication.Application.Interfaces;
using JobApplication.Infrastructure.Data;
using JobApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Job Application API",
        Version = "v1",
        Description = "API for managing job postings and candidate applications."
    });

    // Include XML comments from this assembly
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("AppSystem")));

builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

//builder.Services.AddScoped<JobService>();
//builder.Services.AddScoped<ApplicationService>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(JobApplication.Application.AssemblyReference).Assembly));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
