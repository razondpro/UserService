using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Shared.Infrastructure.Persistence.Core.Outbox;

namespace UserService.Shared.Infrastructure.Persistence.Models
{
    public class OutboxMessagesConsumerModelConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
    {
        public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
        {
            builder.ToTable("outbox_messages_consumer");

            builder.Property(obc => obc.Id)
                .HasColumnName("id");

            builder.HasKey(obc => obc.Id);

            builder.Property(obc => obc.EventId)
                .HasColumnName("event_id");

            builder.Property(obc => obc.EventType)
                .HasColumnName("event_type");

            builder.Property(obc => obc.Timestamp)
                .HasColumnName("timestamp");

            builder.HasIndex(obc => new { obc.EventId, obc.EventType });
        }
    }
}