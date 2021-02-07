﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Domain.Interfaces
{
	public interface IEntity
	{
		Guid Id { get; }
		DateTime CreatedAt { get; }
		DateTime UpdatedAt { get; }

		void Validate();
	}
}
