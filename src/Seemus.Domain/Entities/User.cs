using Microsoft.AspNetCore.Identity;
using Seemus.Domain.Constants;
using Seemus.Domain.Core;
using Seemus.Domain.Dtos.User;
using Seemus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Seemus.Domain.Entities
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public string Name { get; private set; }

        public string RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiration { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public virtual Artist Artist { get; private set; }
        public virtual IList<UserRole> UserRoles { get; private set; }
        public virtual IList<UserLogin> Logins { get; private set; }
        public virtual IList<UserClaim> Claims { get; private set; }
        public virtual IList<UserToken> Tokens { get; private set; }

        protected User()
        { }

        public User(string name, string email) : base(email)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            Name = name;
            Email = email;
            EmailConfirmed = true;
        }

        public bool HasRole(string roleName)
        {
            if (UserRoles is null || UserRoles.Count == 0)
                return false;

            return UserRoles.Any(x => x.Role.Name.Equals(roleName));
        }

        public void CreateArtistData()
        {
            if (!HasRole(Roles.Artist))
                throw new InvalidOperationException("Only users in artist role can create artist data.");

            Artist = new Artist(this);
        }

        public void AddRole(Role role)
        {
            Validations.IsNull(role, nameof(role));
            UserRoles ??= new List<UserRole>();

            if (!UserRoles.Any(x => x.RoleId.Equals(role.Id)))
                UserRoles.Add(new UserRole(this, role));
        }

        public void Validate()
        {
            Validations.IsNullOrEmpty(Name, nameof(Name));
            Validations.IsNullOrEmpty(Email, nameof(Email));
            Validations.IsFalse(new EmailAddressAttribute().IsValid(Email), $"The property {nameof(Email)} is not valid e-mail");
        }

        public void AddRefreshToken(string refreshToken, DateTime expiration)
        {
            Validations.IsNullOrEmpty(refreshToken, nameof(refreshToken));
            Validations.IsTrue(expiration < DateTime.Now, "A data de expiração deve ser uma data futura");

            RefreshToken = refreshToken;
            RefreshTokenExpiration = expiration;
        }

        public void UpdateProfile(UpdateUserProfileDto dto)
        {
            _ = dto ?? throw new ArgumentNullException(nameof(dto));

            Name = dto.Name;

            Validate();
        }
    }
}
