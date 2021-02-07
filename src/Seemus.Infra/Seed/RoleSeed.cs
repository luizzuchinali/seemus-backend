using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seemus.Domain.Entities;
using System;

namespace Seemus.Infra.Seed
{
	public static class RoleSeed
	{
		public static void Seed(this EntityTypeBuilder<Role> builder)
		{
			var date = new DateTime(2021, 2, 7);
			var roles = new[]
			{
				new Role(new Guid("7fe87d14-1645-4f82-9c97-fa602b9ad9f6"), "artist", date, date),
			};

			builder.HasData(roles);
		}
	}
}
