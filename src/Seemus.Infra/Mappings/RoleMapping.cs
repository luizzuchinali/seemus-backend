using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seemus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Infra.Mappings
{
	public class RoleMapping : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasMany(x => x.UserRoles)
				.WithOne(x => x.Role)
				.HasForeignKey(x => x.RoleId);

			builder.HasMany(x => x.Claims)
				.WithOne(x => x.Role)
				.HasForeignKey(x => x.RoleId);

			builder.ToTable("Roles");
		}
	}
}
