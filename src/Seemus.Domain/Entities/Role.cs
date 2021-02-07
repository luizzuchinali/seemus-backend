using Microsoft.AspNetCore.Identity;
using Seemus.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Seemus.Domain.Entities
{
	public class Role : IdentityRole<Guid>, IEntity
	{
		public DateTime CreatedAt { get; private set; }

		public DateTime UpdatedAt { get; private set; }

		public IList<User> Users { get; private set; }

		protected Role()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = DateTime.UtcNow;
		}

		public void Validate()
		{
			throw new NotImplementedException();
		}
	}
}
