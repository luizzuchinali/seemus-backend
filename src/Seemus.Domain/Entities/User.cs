using Microsoft.AspNetCore.Identity;
using Seemus.Domain.Core;
using Seemus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Seemus.Domain.Entities
{
	public class User : IdentityUser<Guid>, IEntity
	{
		public string Name { get; private set; }

		public string RefreshToken { get; private set; }
		public DateTime RefreshTokenExpiration { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime UpdatedAt { get; private set; }

		public virtual IList<UserRole> UserRoles { get; private set; }
		public virtual IList<UserLogin> Logins { get; private set; }
		public virtual IList<UserClaim> Claims { get; private set; }
		public virtual IList<UserToken> Tokens { get; private set; }

		protected User()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = DateTime.UtcNow;
		}

		public User(string name, string email) : base(email)
		{
			Name = name;
			Email = email;
			EmailConfirmed = true;
		}

		public void AddRole(Role role)
		{
			Validations.IsNull(role, nameof(role));
			UserRoles ??= new List<UserRole>();

			if (!UserRoles.Any(x => x.RoleId.Equals(role.Id)))
				UserRoles.Add(new UserRole(this, role));
		}

		public void Validate()
		{
			Validations.IsNullOrEmpty(Name, nameof(Name));
			Validations.IsNullOrEmpty(Email, nameof(Email));
			Validations.IsFalse(new EmailAddressAttribute().IsValid(Email), $"The property {nameof(Email)} is not valid e-mail");
		}

		public void AddRefreshToken(string refreshToken, DateTime expiration)
		{
			Validations.IsNullOrEmpty(refreshToken, nameof(refreshToken));
			Validations.IsTrue(expiration < DateTime.Now, "A data de expiração deve ser uma data futura");

			RefreshToken = refreshToken;
			RefreshTokenExpiration = expiration;
		}
	}
}
