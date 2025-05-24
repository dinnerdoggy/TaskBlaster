using Microsoft.EntityFrameworkCore;
using TaskBlaster.Data;
using TaskBlaster.Interfaces;
using TaskBlaster.Repositories;
using TaskBlaster.Services;
using TaskBlaster.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TaskBlasterDbConnectionString");
builder.Services.AddDbContext<TaskBlasterDbContext>(options => options.UseNpgsql(connectionString));

// Registering the repository and service
builder.Services.AddScoped<IDutyRepository, DutyRepository>();
builder.Services.AddScoped<IDutyService, DutyService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDutyEndpoints();

app.Run();
