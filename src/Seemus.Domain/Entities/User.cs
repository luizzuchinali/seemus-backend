﻿using Microsoft.AspNetCore.Identity;
using Seemus.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Seemus.Domain.Entities
{
	public class User : IdentityUser<Guid>, IEntity
	{
		public string Name { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime UpdatedAt { get; private set; }

		public IList<Role> Roles { get; private set; }

		protected User()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = DateTime.UtcNow;
		}

		public User(string name, string email)
		{
			Name = name;
			Email = email;
			EmailConfirmed = true;
		}

		public void Validate()
		{
			throw new System.NotImplementedException();
		}
	}
}
