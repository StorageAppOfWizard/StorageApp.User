using Microsoft.EntityFrameworkCore;
using StorageApp.User.Api.Configurations;
using StorageApp.User.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationConfiguration(builder.Configuration);
builder.Services.AddInfrastructureConfiguration(builder.Configuration);
builder.Services.AddAuthConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StorageApp.User API V1");
        c.RoutePrefix = string.Empty;
    });

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate(); // Isso executa as migrations pendentes
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Erro ao aplicar migrations");
        }
    }
}

app.UseCustomMiddlewares();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
