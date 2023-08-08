
namespace UserService.Shared.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;

    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {

        }
    }
}