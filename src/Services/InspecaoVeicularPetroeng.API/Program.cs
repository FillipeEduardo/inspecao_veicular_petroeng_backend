using InspecaoVeicularPetroeng.API.Configuration;
using InspecaoVeicularPetroeng.API.Helpers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
DotEnv.Load();
builder.Services.AddApiConfiguration(builder.Environment);
builder.Host.UseSerilog();

var app = builder.Build();
app.UseApiConfiguration();

app.Run();