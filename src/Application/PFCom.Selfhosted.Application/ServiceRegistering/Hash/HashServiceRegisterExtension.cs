using Microsoft.Extensions.DependencyInjection;
using PFCom.Selfhosted.Application.Hash.Hashing;
using PFCom.Selfhosted.Application.Hash.Hashing.Impl;
using PFCom.Selfhosted.Application.Hash.Password;
using PFCom.Selfhosted.Application.Hash.Password.Impl;

namespace PFCom.Selfhosted.Application.ServiceRegistering.Hash
{
    public static class HashServiceRegisterExtension
    {
        public static void RegisterHashServices(IServiceCollection services)
        {
            registerHashing(services);
            registerPassword(services);
        }

        private static void registerHashing(IServiceCollection services)
        {
            services.AddSingleton<IHashService, HashService>();
        }

        private static void registerPassword(IServiceCollection services)
        {
            services.AddSingleton<IUserRegisterPasswordHashService, UserRegisterPasswordHashService>();
        }
    }
}
