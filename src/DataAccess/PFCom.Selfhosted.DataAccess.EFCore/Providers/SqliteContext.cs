using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PFCom.Selfhosted.DataAccess.EFCore.Providers
{
    public class SqliteContext : DataContext
    {
        public SqliteContext(IConfiguration configuration) : base(configuration.GetConnectionString("DefaultConnection"))
        {
        }
        
        public SqliteContext(string connectionString) : base(connectionString)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(this.ConnStr);
        }
    }
}
