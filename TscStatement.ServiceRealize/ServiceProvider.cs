using Microsoft.Extensions.DependencyInjection;
using TscStatement.Abstract.IService;
using TscStatement.EntityFramework;

namespace TscStatement.ServiceRealize
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection service, string dataBase)
        {
            service.AddScoped<InjectionPoint>();
            service.AddScoped(e => new DbContext(dataBase));
            return service.AddScoped<IOrderInfoService, OrderInfoService>();
        }
    }
}