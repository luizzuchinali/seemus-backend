using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Domain.Interfaces
{
	public interface ITokenFactory
	{
		string GenerateRefreshToken(int size = 32);
	}
}
