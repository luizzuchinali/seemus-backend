using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seemus.Domain.Dtos.User;
using Seemus.Domain.Entities;

namespace Seemus.Domain.Interfaces.Data
{
    public interface IUserRepository : IRepository<User>
    {
        Task<Artist> GetArtistByUserId(Guid userId);
        Task<IEnumerable<ArtistDto>> GetAllArtists(string search, bool online = true);
    }
}