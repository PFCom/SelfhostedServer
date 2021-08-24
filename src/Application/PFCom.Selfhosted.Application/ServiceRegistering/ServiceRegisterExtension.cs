using Microsoft.Extensions.DependencyInjection;

namespace PFCom.Selfhosted.Application.ServiceRegistering
{
    public static class ServiceRegisterExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            Hash.HashServiceRegisterExtension.RegisterHashServices(services);
            User.UserServiceRegisterExtension.RegisterUserServices(services);
        }
    }
}
