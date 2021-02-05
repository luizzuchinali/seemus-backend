using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Api.Configurations
{
	public static class AuthenticationConfig
	{
		public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			//Configure Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(config =>
			{
				config.RequireHttpsMetadata = false;
				config.SaveToken = true;

				config.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = configuration["JwtTokenConfig:Issuer"],
					ValidateIssuer = false,
					ValidateAudience = bool.Parse(configuration["JwtTokenConfig:ValidateAudience"]),
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtTokenConfig:Key"])),
					ClockSkew = TimeSpan.Zero
				};
			});

		}
	}
}
