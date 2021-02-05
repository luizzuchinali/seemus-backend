using Seemus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Seemus.Api.Authentication
{
	public class TokenFactory : ITokenFactory
	{
		public string GenerateRefreshToken(int size = 32)
		{
			var randomNumber = new byte[size];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
	}
}
