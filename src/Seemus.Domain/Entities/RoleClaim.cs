using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Domain.Entities
{
	public class RoleClaim : IdentityRoleClaim<Guid>
	{
		public virtual Role Role { get; private set; }
	}
}
