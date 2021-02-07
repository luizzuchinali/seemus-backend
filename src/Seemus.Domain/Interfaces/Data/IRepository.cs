using Luizanac.Utils.Extensions.Interfaces;
using Seemus.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seemus.Domain.Interfaces.Data
{
	public interface IRepository<T> : IDisposable where T : IEntity
	{
		Task<T> Get(Expression<Func<T, bool>> func);
		Task<T> GetById(Guid id);
		Task<IList<T>> GetByIds(Guid[] ids);
		Task<bool> Any(Expression<Func<T, bool>> func = null);
		Task<IList<T>> GetAll();
		Task<IList<T>> GetAll(Expression<Func<T, bool>> func = null, string sort = null);
		Task<IPagination<IList<T>>> GetAllPaginated(int page, int size, Expression<Func<T, bool>> func = null, string sort = null);
		Task Add(T t);
		void Update(T t);
		void Remove(T t);
		IUnitOfWork UnitOfWork { get; }
	}
}
