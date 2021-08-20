using Microsoft.Extensions.DependencyInjection;

namespace PFCom.Selfhosted.DataAccess
{
    public static class RepositoryRegisterExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            //services.AddScoped(typeof(  HERE INTERFACE OF REPOSITORY  ), typeof(  HERE THE IMPLEMENTATION CLASS OF REPOSITORY  ));
        }
    }
}
