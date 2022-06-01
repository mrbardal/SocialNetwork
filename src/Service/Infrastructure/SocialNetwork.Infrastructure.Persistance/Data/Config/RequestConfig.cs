using SocialNetwork.Domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialNetwork.Infrastructure.Persistance.Data.Config
{
    public class AccountConfig : IEntityTypeConfiguration<Product>
    {
        private const string DatabaseSchema = "Sales";
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", SqlContext.DatabaseSchema);
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseHiLo("ProductSeq", SqlContext.DatabaseSchema);
            builder.Property(m => m.Name).IsRequired().IsUnicode();
            builder.Property(m => m.Description).IsRequired(false).HasMaxLength(200).IsUnicode();
        }
    }
}
