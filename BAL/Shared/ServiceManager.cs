using BAL.ISercices;
using BAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MODEL;
using MODEL.ApplicationConfig;
using REPOSITORY.UnitOfWork;

namespace BAL.Shared
{
    public class ServiceManager
    {
        public static void SetServicesInfo(IServiceCollection services, Appsetting appSettings)
        {
            services.AddDbContextPool<DataContext>(options => {
                options.UseSqlServer(appSettings.ConnectionStrings);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductServices, ProductServices>();
        }
    }
}
