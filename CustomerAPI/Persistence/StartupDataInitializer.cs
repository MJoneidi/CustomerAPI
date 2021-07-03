using CustomerAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CustomerAPI.Persistence
{
    public static class StartupDataInitializer
    {
        public static void Initiate(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var customerCacheService = serviceScope.ServiceProvider.GetService<ICustomerCacheService>();
                customerCacheService.InitiateAsync();
            }
        }
    }
}
