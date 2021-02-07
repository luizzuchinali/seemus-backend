using Seemus.Api.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Api.Dtos.Account
{
	public class AuthResultDto
	{
		public string TokenType { get; set; }
		public string Token { get; set; }
		public string RefreshToken { get; set; }
		public DateTime Expiration { get; set; }
		public UserDto User { get; set; }

		public AuthResultDto(string tokenType, string token, string refreshToken, DateTime expiration, UserDto user)
		{
			TokenType = tokenType;
			Token = token;
			RefreshToken = refreshToken;
			Expiration = expiration;
			User = user;
		}
	}
}
