using Seemus.Domain.Core;

namespace Seemus.Domain.Entities
{
	public class User : Entity
	{
		public string Email { get; private set; }

		public override void Validate()
		{
			throw new System.NotImplementedException();
		}
	}
}
