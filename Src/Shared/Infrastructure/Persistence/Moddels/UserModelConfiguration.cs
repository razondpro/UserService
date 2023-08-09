namespace UserService.Shared.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Modules.User.Domain.Entities;
    using Shared.Domain;
    using UserService.Modules.User.Domain.ValueObjects;

    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // primay key
            builder.HasKey(user => user.Id);
            //index
            builder.HasIndex(user => user.Email)
                .IsUnique();
            builder.HasIndex(user => user.UserName)
                .IsUnique();
            // properties
            builder.Property(user => user.Id)
                .HasConversion(id => id.Value,
                value => new UniqueIdentity(value));

            builder.Property(user => user.Email)
                .HasMaxLength(Email.MaxLength)
                .IsRequired()
                .HasConversion(email => email.Value,
                value => Email.Create(value));

            builder.Property(user => user.UserName)
                .HasMaxLength(UserName.MaxLength)
                .IsRequired()
                .HasConversion(userName => userName.Value,
                value => UserName.Create(value));

            builder.Property(user => user.FirstName)
                .HasMaxLength(FirstName.MaxLength)
                .IsRequired()
                .HasConversion(firstName => firstName.Value,
                value => FirstName.Create(value));

            builder.Property(user => user.LastName)
            .HasMaxLength(LastName.MaxLength)
                .HasConversion(lastName => lastName.Value,
                value => LastName.Create(value));

        }
    }
}