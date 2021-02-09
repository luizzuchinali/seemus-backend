using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seemus.Domain.Dtos.User;
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

        public async Task<IEnumerable<ArtistDto>> GetAllArtists(string search, bool online = true) =>
            await DbContext.Artists.Include(x => x.User).AsNoTracking()
                .Where(x => EF.Functions.Like(x.User.Name, $"%{search}%") && x.Online.Equals(online))
                .OrderBy(x => x.User.Name)
                .Select(x => new ArtistDto(x.UserId, x.User.Name, x.ProfileImageUrl, x.Online))
                .ToListAsync();


    }
}