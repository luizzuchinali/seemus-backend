using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Domain.Core
{
	public interface IValueObject
	{
		void Validate();
	}
}
