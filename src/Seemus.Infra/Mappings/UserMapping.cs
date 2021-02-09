using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seemus.Domain.Entities;
using System.Collections.Generic;

namespace Seemus.Infra.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(x => x.Artist).WithOne(x => x.User).HasForeignKey<Artist>(x => x.UserId);

            //Users : Roles
            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Claims)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Logins)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Tokens)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.ToTable("Users");
        }
    }
}
