using System;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("PersonsDb"));

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PersonsAPIConnectionString")));

var app = builder.Build();


var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<AppDbContext>();
db.InitializedAsync().GetAwaiter().GetResult();


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
