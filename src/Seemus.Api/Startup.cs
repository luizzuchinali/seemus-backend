using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Seemus.Api.Configurations;
using Seemus.Infra;
using System.Net.Mime;

namespace Seemus.Api
{
	public class Startup
	{
		public Startup(IHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			//Configura acesso a dados da aplicação
			services.AddDatabaseConfiguration(Configuration);

			//Configura autenticação
			services.AddAuthenticationConfiguration(Configuration);

			//Configura documentação do swagger
			services.AddSwaggerConfiguration();

			//Configura automapper
			services.AddAutoMapperConfiguration();

			//Abstrações
			services.AddDependencyInjectionConfiguration();

			services.AddControllers().ConfigureApiBehaviorOptions(options =>
			{
				options.InvalidModelStateResponseFactory = context =>
				{
					var result = new BadRequestObjectResult(new AppProblemDetails(context.ModelState));
					result.ContentTypes.Add(MediaTypeNames.Application.Json);
					return result;
				};
			});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			UpdateDatabase(app);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwaggerSetup();
			}

			app.UseCors(env);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseResponseCaching();
			app.UseResponseCompression();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static void UpdateDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices
				.GetRequiredService<IServiceScopeFactory>()
				.CreateScope())
			{
				using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
				{
					context.Database.Migrate();
				}
			}
		}
	}
}
