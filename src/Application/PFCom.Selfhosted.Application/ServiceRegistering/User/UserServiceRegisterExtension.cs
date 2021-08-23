using Microsoft.Extensions.DependencyInjection;
using PFCom.Selfhosted.Application.User.Registrar.Local;
using PFCom.Selfhosted.Application.User.Registrar.Local.Impl;

namespace PFCom.Selfhosted.Application.ServiceRegistering.User
{
    public class UserServiceRegisterExtension
    {
        public static void RegisterUserServices(IServiceCollection services)
        {
            registerRegistrars(services);
        }

        private static void registerRegistrars(IServiceCollection services)
        {
            services.AddScoped<ILocalUserRegistrar, LocalUserRegistrar>();
        }
    }
}
