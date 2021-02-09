using AutoMapper;
using Seemus.Domain.Dtos.User;
using Seemus.Domain.Entities;
using System.Linq;

namespace Seemus.Api.AutoMapper
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.Role.Name)));
		}
	}
}
