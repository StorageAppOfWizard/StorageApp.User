using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using StorageApp.User.Application.Contracts;
using StorageApp.User.Application.Security;
using StorageApp.User.Application.Services;
using StorageApp.User.Domain.Contracts;
using StorageApp.User.Infrastructure.Data;
using StorageApp.User.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();



var connectionstring = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionstring));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IHasherPassword,HasherPassword>();
builder.Services.AddTransient<IJwtService,JwtService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "StorageApp.User API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
