using Hangfire;
using JobApplication.Application.Interfaces;
using JobApplication.Infrastructure.Data;
using JobApplication.Infrastructure.Hangfire;
using JobApplication.Infrastructure.Repositories;
using JobApplication.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ────────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── Swagger / OpenAPI ──────────────────────────────────────────────────────────
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Job Application API",
        Version = "v1",
        Description = "API for managing job postings and candidate applications."
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddOpenApi();

// ── Database ───────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("AppSystem")));

// ── Repositories ───────────────────────────────────────────────────────────────
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

// ── MediatR ────────────────────────────────────────────────────────────────────
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(JobApplication.Application.AssemblyReference).Assembly));

// ── Hangfire ───────────────────────────────────────────────────────────────────
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("AppSystem")));

// Hangfire server — processes background jobs
builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 2;   // lightweight; increase if needed
});

// Recurring job service (resolved by Hangfire's DI-aware activator)
builder.Services.AddScoped<IRecurringJobService, RecurringJobService>();

// ──────────────────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Swagger UI ─────────────────────────────────────────────────────────────────
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ── Hangfire Dashboard (available at /hangfire) ────────────────────────────────
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    // ⚠️ In production: add an authorization filter here!
    // Authorization = new[] { new HangfireCustomBasicAuthFilter() }
    DashboardTitle = "Job Application — Background Jobs"
});

// ── Register Recurring Jobs ────────────────────────────────────────────────────
var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
recurringJobManager.RegisterRecurringJobs();

// ── Middleware ─────────────────────────────────────────────────────────────────
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
