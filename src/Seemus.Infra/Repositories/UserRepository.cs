using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seemus.Domain.Entities;
using Seemus.Domain.Interfaces.Data;

namespace Seemus.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<User> GetById(Guid userId) =>
            await DbContext.Users.Include(x => x.Artist).SingleOrDefaultAsync(x => x.Id.Equals(userId));

        public async Task<Artist> GetArtistByUserId(Guid userId) =>
            await DbContext.Artists.Include(x => x.User).SingleOrDefaultAsync(x => x.UserId.Equals(userId));
    }
}