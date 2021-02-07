using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seemus.Domain.Entities;

namespace Seemus.Infra.Mappings
{
	public class RoleClaimMapping : IEntityTypeConfiguration<RoleClaim>
	{
		public void Configure(EntityTypeBuilder<RoleClaim> builder)
		{

			builder.ToTable("RoleClaims");
		}
	}
}
