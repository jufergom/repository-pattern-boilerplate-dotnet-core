using Boilerplate.Services;
using Boilerplate.Services.Interfaces;
using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add DbContext services
var dbPath = builder.Configuration.GetConnectionString("BloggingDatabase");
builder.Services.AddSqlite<ApplicationContext>(dbPath);

//add repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//add entity services
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
