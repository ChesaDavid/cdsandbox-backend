using cdsandbox.Backend.Data;
using cdsandbox.Backend.Models;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
// var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
// var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
// var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
        .UseNpgsql(connectionString));

builder.Services.AddSignalR();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

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

app.UseCors("AngularClient");

app.UseAuthorization();

app.MapControllers();

app.Run();

