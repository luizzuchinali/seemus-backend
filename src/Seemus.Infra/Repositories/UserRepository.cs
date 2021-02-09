using System;
using System.Threading.Tasks;
using Seemus.Domain.Entities;
using Seemus.Domain.Interfaces.Data;

namespace Seemus.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Artist> GetArtistByUserId(Guid userId) => await DbContext.Artists.FindAsync(userId);
    }
}