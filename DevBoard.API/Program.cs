using DevBoard.Application.Interfaces;
using DevBoard.Application.Services;
using DevBoard.Infrastructure.Messaging;
using DevBoard.Infrastructure.Persistence;
using DevBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ProjectService>();

builder.Services.AddScoped<IEventPublisher, NoOpEventPublisher>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Automatically open localhost:5000
if (app.Environment.IsDevelopment())
{
    var url = "http://localhost:5000";

    Task.Run(async () =>
    {
        await Task.Delay(1000);

        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    });
}
app.Run();