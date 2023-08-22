using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Shared.Domain.Events;

namespace UserService.Shared.Infrastructure.Persistence.Moddels
{
    public class OutboxMessagesModelConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_messages");

            builder.HasKey(user => user.Id);

            builder.HasIndex(user => user.Type);

            builder.Property(user => user.Id)
                .HasColumnName("id");

            builder.Property(user => user.Type)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(user => user.Data)
                .HasColumnName("data")
                .IsRequired();

            builder.Property(user => user.OccurredOn)
                .HasColumnName("occurred_on")
                .IsRequired();

            builder.Property(user => user.ProcessedOn)
                .HasColumnName("processed_on");

            builder.Property(user => user.Error)
                .HasColumnName("error");

        }
    }
}