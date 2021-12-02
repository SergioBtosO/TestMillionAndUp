using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MillionAndUp.Core.Application.Behaviours;
using System.Reflection;

namespace MillionAndUp.Core.Aplication
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer (this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof (IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
