﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Domain.Dtos.User
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}
