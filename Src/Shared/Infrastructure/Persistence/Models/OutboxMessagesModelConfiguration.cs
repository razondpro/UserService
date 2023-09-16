namespace UserService.Shared.Infrastructure.Persistence.Moddels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using UserService.Shared.Infrastructure.Persistence.Core.Outbox;

    public class OutboxMessagesModelConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_messages");

            builder.HasKey(ob => ob.Id);

            builder.HasIndex(ob => ob.Type);

            builder.Property(ob => ob.Id)
                .HasColumnName("id");

            builder.Property(ob => ob.Type)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(ob => ob.Data)
                .HasColumnName("data")
                .IsRequired();

            builder.Property(ob => ob.OccurredOn)
                .HasColumnName("occurred_on")
                .IsRequired();

            builder.Property(ob => ob.ProcessedOn)
                .HasColumnName("processed_on");

            builder.Property(ob => ob.Error)
                .HasColumnName("error");

        }
    }
}