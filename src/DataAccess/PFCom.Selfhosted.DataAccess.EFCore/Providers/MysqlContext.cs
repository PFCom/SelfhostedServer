using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PFCom.Selfhosted.DataAccess.EFCore.Providers
{
    public class MysqlContext : DataContext
    {
        public MysqlContext(IConfiguration configuration) : base(configuration.GetConnectionString("DefaultConnection"))
        {
        }
        
        public MysqlContext(string connectionString) : base(connectionString)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(this.ConnStr, ServerVersion.AutoDetect(this.ConnStr));
        }
    }
}
