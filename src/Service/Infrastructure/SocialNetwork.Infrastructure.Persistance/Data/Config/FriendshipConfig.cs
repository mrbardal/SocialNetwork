using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Domain.Aggregates.FriendshipAggregate;

namespace SocialNetwork.Infrastructure.Persistance.Data.Config;

public class FriendshipConfig : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.ToTable("Friendship", AppContextDb.DatabaseSchema);
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).UseHiLo("FriendshipSeq", AppContextDb.DatabaseSchema);
        builder.Property(m => m.Requester).IsRequired();
        builder.Property(m => m.Addressee).IsRequired();

        //builder
        //    .Property<int>("_friendshipStatusId")
        //    // .HasField("_orderStatusId")
        //    .UsePropertyAccessMode(PropertyAccessMode.Field)
        //    .HasColumnName("FriendshipStatusId")
        //    .IsRequired();

        //builder
        //    .HasOne(o => o.Status)
        //    .WithMany()
        //    // .HasForeignKey("OrderStatusId");
        //    .HasForeignKey("_friendshipStatusId");
    }
}
