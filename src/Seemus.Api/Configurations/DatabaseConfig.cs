using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seemus.Api.Validations;
using Seemus.Domain.Entities;
using Seemus.Infra;
using System;

namespace Seemus.Api.Configurations
{
	public static class DatabaseConfig
	{
		public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseLazyLoadingProxies();
				options.UseSqlServer(configuration.GetConnectionString("Default"));
				options.EnableDetailedErrors();
				options.EnableSensitiveDataLogging();
			});

			services.AddIdentity<User, Role>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 8;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.SignIn.RequireConfirmedEmail = true;
				options.SignIn.RequireConfirmedPhoneNumber = false;
				options.User.RequireUniqueEmail = true;
			})
				.AddErrorDescriber<PortugueseIdentityErrorDescriber>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
		}
	}
}
