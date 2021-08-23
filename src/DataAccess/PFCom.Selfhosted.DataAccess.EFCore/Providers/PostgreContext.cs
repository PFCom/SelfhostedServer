using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PFCom.Selfhosted.DataAccess.EFCore.Providers
{
    public class PostgreContext : DataContext
    {
        public PostgreContext(IConfiguration configuration) : base(configuration.GetConnectionString("DefaultConnection"))
        {
        }
        
        public PostgreContext(string connectionString) : base(connectionString)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(this.ConnStr);
        }
    }
}
