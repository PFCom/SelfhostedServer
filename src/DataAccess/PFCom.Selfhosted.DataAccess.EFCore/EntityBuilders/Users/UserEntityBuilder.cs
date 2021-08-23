using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFCom.Selfhosted.Core.Users;

namespace PFCom.Selfhosted.DataAccess.EFCore.EntityBuilders.Users
{
    public class UserEntityBuilder
    {
        public void EntityConfiguration(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nickname)
                .HasMaxLength(32);

            builder.Ignore(x => x.Type);
            builder.Property(x => x.Type_str)
                .HasColumnName("Type")
                .HasMaxLength(16);
        }
    }
}
