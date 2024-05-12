using Microsoft.EntityFrameworkCore;

namespace SignalR_Chat
{
    public class MessageContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<M_UserMessage> Messages { get; set; }
        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                SaveChanges();
            }
        }
    }
}
