using Application.Behaviors;
using Application.Features.Service;

//using Application.Features.Handlers.AuthHandler;
using Application.Interfaces;
using Application.Interfaces.IEmailService;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application
{
    public static class DIExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<UserManager<Customer>>();
            services.AddSingleton<CloudinaryService>();
            services.AddSingleton<IRazorpayService, RazorPayService>();
            services.AddScoped<IEmailService, EmailService>();

        }
    }
}
