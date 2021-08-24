using Microsoft.EntityFrameworkCore;
using PFCom.Selfhosted.Core.Users;

namespace PFCom.Selfhosted.DataAccess.EFCore
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<LocalUser> LocalUsers { get; set; }
        
        protected string ConnStr { get; }
        
        public DataContext(string connectionString)
        {
            this.ConnStr = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // USERS
            new EntityBuilders.Users.UserEntityBuilder().EntityConfiguration(builder.Entity<User>());
            new EntityBuilders.Users.LocalUserEntityBuilder().EntityConfiguration(builder.Entity<LocalUser>());
        }
    }
}
