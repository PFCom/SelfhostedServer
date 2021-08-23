using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFCom.Selfhosted.Core.Users;

namespace PFCom.Selfhosted.DataAccess.EFCore.EntityBuilders.Users
{
    public class LocalUserEntityBuilder
    {
        public void EntityConfiguration(EntityTypeBuilder<LocalUser> builder)
        {
            builder.ToTable("LocalUser");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<LocalUser>(x => x.Id);
            builder.Navigation(x => x.User).AutoInclude();
        }
    }
}
