using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

			services.AddCors(options =>
			{
				options.AddPolicy("DevlopmentCors", p =>
				{
					p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.WithExposedHeaders("X-Total-Count", "Link");
				});

				options.AddPolicy("ProductionCors", p =>
				{
					p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.WithExposedHeaders("X-Total-Count", "Link");
				});
			});

			services.AddHttpContextAccessor();

			//Repositorios
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

			//Auth
			services.AddSingleton<ITokenFactory, TokenFactory>();

			//Configure responseCompression
			services.AddResponseCompression().Configure<BrotliCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Optimal;
			});
		}

		public static void UseCors(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (app == null) throw new ArgumentNullException(nameof(app));

			if (env.IsDevelopment())
			{
				app.UseCors("DevelopmentCors");
			}
			else
			{
				app.UseCors("ProductionCors");
			}
		}
	}
}
