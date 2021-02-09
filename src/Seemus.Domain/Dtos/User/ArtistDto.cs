using System;

namespace Seemus.Domain.Dtos.User
{
    public class ArtistDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool Online { get; set; }
    }
}