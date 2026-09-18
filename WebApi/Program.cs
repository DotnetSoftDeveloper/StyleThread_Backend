
using Application;
using InfraStructure;
using Microsoft.OpenApi.Models;
using InfraStructure.Middlewares;
using Serilog;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddApplication();
                builder.Services.AddInfraStructure(builder.Configuration);
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                    // Add security definition for bearer token
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter your bearer token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                    });
                });

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    });
                }

                app.UseMiddleware<ExceptionHandlingMiddleware>(); // Should wrap everything to catch exceptions

                app.UseHttpsRedirection();

                app.UseCors(options =>
                {
                    //options.WithOrigins("http://localhost:5176").AllowAnyMethod().AllowAnyHeader();
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });

                app.UseRouting();

                app.UseAuthentication(); // Must come before Authorization

                app.UseAuthorization();  // Required before endpoint execution


                app.UseMiddleware<CustomAuthorizationMiddleware>(); // Now it can catch 401s properly

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
