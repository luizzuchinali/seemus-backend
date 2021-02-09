using System;
using Seemus.Domain.Core;

namespace Seemus.Domain.Entities
{
    public class Artist
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }
        public string ProfileImageUrl { get; set; }
        public bool Online { get; set; }

        protected Artist()
        { }

        public Artist(User user)
        {
            Validations.IsNull(user, nameof(user));

            User = user;
            UserId = user.Id;
        }

        public Artist(User user, string profileImageUrl) : this(user)
        {
            Validations.IsNullOrEmpty(profileImageUrl, nameof(profileImageUrl));

            ProfileImageUrl = profileImageUrl;
        }

        public void ChangeOnlineStatus(bool status) => Online = status;

    }
}