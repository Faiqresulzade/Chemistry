using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(125);

            builder.Property(e => e.Pdf)
                .HasMaxLength(125);

            builder.Property(e => e.Link)
                .HasMaxLength(125);
        }
    }
}
