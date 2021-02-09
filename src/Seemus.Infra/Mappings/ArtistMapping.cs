using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seemus.Domain.Entities;

namespace Seemus.Infra.Mappings
{
    public class ArtistMapping : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(x => x.UserId);
            
            builder.Property(x => x.ProfileImageUrl).HasColumnType("text");
            builder.HasOne(x => x.User).WithOne(x => x.Artist).HasForeignKey<Artist>(x => x.UserId);

            builder.ToTable("Artists");
        }
    }
}