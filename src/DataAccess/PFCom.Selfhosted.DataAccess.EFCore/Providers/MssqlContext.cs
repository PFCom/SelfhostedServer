using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PFCom.Selfhosted.DataAccess.EFCore.Providers
{
    public class MssqlContext : DataContext
    {
        public MssqlContext(IConfiguration configuration) : base(configuration.GetConnectionString("DefaultConnection"))
        {
        }
        
        public MssqlContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnStr);
        }
    }
}
