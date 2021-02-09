using System;
using System.Threading.Tasks;
using Seemus.Domain.Entities;

namespace Seemus.Domain.Interfaces.Data
{
    public interface IUserRepository : IRepository<User>
    {
        Task<Artist> GetArtistByUserId(Guid userId);
    }
}