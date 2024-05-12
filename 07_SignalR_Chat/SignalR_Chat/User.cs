using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SignalR_Chat
{
    public class User
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string? Name { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
