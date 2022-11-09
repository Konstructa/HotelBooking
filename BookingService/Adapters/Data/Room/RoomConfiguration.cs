using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;


namespace Data.Room
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Entities.Room>
    {
        public void Configure(EntityTypeBuilder<Entities.Room> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Price).Property(x => x.Currency);
            builder.OwnsOne(x => x.Price).Property(x => x.Value);
        }
    }
}
