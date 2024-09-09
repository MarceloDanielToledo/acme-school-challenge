using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Payment.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSharedPaymentServices(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
