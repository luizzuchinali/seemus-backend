using Luizanac.Utils.Extensions;
using Luizanac.Utils.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Seemus.Domain.Interfaces;
using Seemus.Domain.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seemus.Infra.Repositories
{
	public class Repository<T> : IRepository<T> where T : class, IEntity
	{
		protected readonly ApplicationDbContext DbContext;
		protected readonly DbSet<T> DbSet;
		public IUnitOfWork UnitOfWork => DbContext;

		public Repository(ApplicationDbContext context)
		{
			DbContext = context;
			DbSet = DbContext.Set<T>();
		}

		public virtual async Task Add(T entity) => await DbSet.AddAsync(entity);

		public virtual void Update(T entity)
		{
			DbSet.Attach(entity);
			DbContext.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Remove(T entity) =>
			DbSet.Remove(entity);


		public virtual async Task<T> GetById(Guid id) => await DbSet.FindAsync(id);

		public virtual async Task<IList<T>> GetByIds(Guid[] ids) =>
			await DbSet.Where(x => ids.Contains(x.Id)).ToListAsync();

		public void Dispose()
		{
			DbContext?.Dispose();
		}

		public virtual async Task<T> Get(Expression<Func<T, bool>> func) => await DbSet.Where(func).SingleOrDefaultAsync();

		public virtual async Task<bool> Any(Expression<Func<T, bool>> func = null)
		{
			if (func is not null)
				return await DbSet.AnyAsync(func);

			return await DbSet.AnyAsync();
		}

		private IQueryable<T> GetAllFiltered(Expression<Func<T, bool>> filtro, string sort = null)
		{
			IQueryable<T> query = DbSet.AsQueryable();

			if (filtro != null)
				query = DbSet.Where(filtro);

			return sort is null ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderByString(sort);
		}

		public virtual async Task<IList<T>> GetAll() => await DbSet.ToListAsync();

		public virtual async Task<IList<T>> GetAll(Expression<Func<T, bool>> func = null, string sort = null) => await GetAllFiltered(func, sort).ToListAsync();

		public virtual async Task<IPagination<IList<T>>> GetAllPaginated(int page, int size, Expression<Func<T, bool>> func = null, string sort = null)
		{
			var query = (IOrderedQueryable<T>)GetAllFiltered(func, sort);
			return await query.Paginate(page, size);
		}
	}
}
