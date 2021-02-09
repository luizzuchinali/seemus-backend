using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Domain.Dtos.Account
{
	public class AuthDto
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
