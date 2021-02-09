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

            CreateMap<User, ArtistDto>()
                .ForMember(x => x.Online, opt => opt.MapFrom(x => x.Artist.Online))
                .ForMember(x => x.ProfileImageUrl, opt => opt.MapFrom(x => x.Artist.ProfileImageUrl));
        }
    }
}
