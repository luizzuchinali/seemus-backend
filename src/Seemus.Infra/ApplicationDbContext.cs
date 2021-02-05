using Microsoft.EntityFrameworkCore;
using Seemus.Domain.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Infra
{
	public class ApplicationDbContext : DbContext, IUnitOfWork
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
				e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
				property.SetColumnType("varchar(100)");

			modelBuilder
				.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

			//Seed database
		}

		public async Task<bool> Commit()
		{
			return await base.SaveChangesAsync() > 0;
		}
	}
}
