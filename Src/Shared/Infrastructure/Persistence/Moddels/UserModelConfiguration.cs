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
            // table name
            builder.ToTable("users");

            // primay key
            builder.HasKey(user => user.Id);
            //index
            builder.HasIndex(user => user.Email)
                .IsUnique();
            builder.HasIndex(user => user.UserName)
                .IsUnique();
            // properties
            builder.Property(user => user.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new UniqueIdentity(value));

            builder.Property(user => user.Email)
                .HasMaxLength(Email.MaxLength)
                .HasColumnName("email")
                .IsRequired()
                .HasConversion(email => email.Value,
                value => Email.Create(value));

            builder.Property(user => user.UserName)
                .HasMaxLength(UserName.MaxLength)
                .HasColumnName("user_name")
                .IsRequired()
                .HasConversion(userName => userName.Value,
                value => UserName.Create(value));

            builder.Property(user => user.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(FirstName.MaxLength)
                .IsRequired()
                .HasConversion(firstName => firstName.Value,
                value => FirstName.Create(value));

            builder.Property(user => user.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(LastName.MaxLength)
                .HasConversion(lastName => lastName.Value,
                value => LastName.Create(value));

        }
    }
}