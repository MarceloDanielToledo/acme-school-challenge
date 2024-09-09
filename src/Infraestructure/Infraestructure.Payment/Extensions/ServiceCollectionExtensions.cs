using Application.Interfaces;
using Infraestructure.ExternalPayment.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.ExternalPayment.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddExternalPaymentServices(this IServiceCollection services)
        {
            services.AddTransient<IPaymentService, MockPaymentService>();
        }
    }
}
