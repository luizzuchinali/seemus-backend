using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Api.Configurations
{
	public static class DatabaseConfig
	{
		public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			//services.AddDbContextPool<ApplicationDbContext>(options =>
			//{
			//    options.UseLazyLoadingProxies();
			//	  options.UseSqlServer();
			//    options.EnableSensitiveDataLogging();
			//});
		}
	}
}
