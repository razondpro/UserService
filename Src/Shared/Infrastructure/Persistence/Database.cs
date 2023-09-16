namespace UserService.Shared.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Shared.Infrastructure.Persistence.Core.Outbox;

    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<OutboxMessageConsumer> OutboxMessagesConsumer { get; set; }
        public Database(DbContextOptions<Database> options) : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
        }
    }
}