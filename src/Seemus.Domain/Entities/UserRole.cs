using Microsoft.AspNetCore.Identity;
using Seemus.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Domain.Entities
{
	public class UserRole : IdentityUserRole<Guid>
	{
		public virtual User User { get; private set; }
		public virtual Role Role { get; private set; }

		public UserRole()
		{

		}

		public UserRole(User user, Role role)
		{
			Validations.IsNull(user, nameof(user));
			Validations.IsNull(role, nameof(role));

			User = user;
			UserId = user.Id;

			Role = role;
			RoleId = role.Id;
		}
	}
}
