using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Seemus.Api.AutoMapper;
using System;

namespace Seemus.Api.Configurations
{
	public static class AutoMapperConfig
	{
		public static void AddAutoMapperConfiguration(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddAutoMapper(typeof(UserProfile));
		}
	}
}
