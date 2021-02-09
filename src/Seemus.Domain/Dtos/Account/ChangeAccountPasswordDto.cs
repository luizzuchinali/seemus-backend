using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Domain.Dtos.Account
{
	public class ChangeAccountPasswordDto
	{
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
		public string RepeatNewPassword { get; set; }
	}
}
