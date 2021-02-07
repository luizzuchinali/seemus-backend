using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Api.Dtos.User
{
	public class RegisterArtistDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(100)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MaxLength(100)]
		public string Password { get; set; }
	}
}
