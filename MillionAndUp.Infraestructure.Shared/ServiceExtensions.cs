using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Infraestructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Infraestructure.Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
