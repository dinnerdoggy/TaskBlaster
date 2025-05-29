using Microsoft.EntityFrameworkCore;
using TaskBlaster.Data;
using TaskBlaster.Interfaces;
using TaskBlaster.Repositories;
using TaskBlaster.Services;
using TaskBlaster.Endpoints;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Set the JSON serializer options to avoid object cycling error
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var connectionString = builder.Configuration.GetConnectionString("TaskBlasterDbConnectionString");
builder.Services.AddDbContext<TaskBlasterDbContext>(options => options.UseNpgsql(connectionString));

// Registering the repository and service
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
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

app.MapCategoryEndpoints();
app.MapDutyEndpoints();

app.Run();
