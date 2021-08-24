using Microsoft.Extensions.DependencyInjection;
using PFCom.Selfhosted.DataAccess.Repositories.Users;
using PFCom.Selfhosted.DataAccess.RepositoryImpls.Users;

namespace PFCom.Selfhosted.DataAccess
{
    public static class RepositoryRegisterExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILocalUserRepository, LocalUserRepository>();
        }
    }
}
