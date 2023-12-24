using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leafy.Application.Services
{
    public static class ServiceRegistration
    {
        // Bu fonksiyon normalde bütün handler'ları program.cs e tek tek eklemek yerine buradan dinamik bir şekilde eklemeye yarıyor. 
        // MediatR ile çalıştığı için önceki handler'ları buraya taşımadık
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
        }  
    }
}
