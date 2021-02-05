using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Seemus.Api.Authentication;
using Seemus.Domain.Interfaces;
using Seemus.Domain.Interfaces.Data;
using Seemus.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Api.Configurations
{
	public static class DependencyInjectionConfig
	{
		public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddHttpContextAccessor();

			//Repositorios
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			//Auth
			services.AddSingleton<ITokenFactory, TokenFactory>();

			//Configure responseCompression
			services.AddResponseCompression().Configure<BrotliCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Optimal;
			});
		}
	}
}
