using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace SignalR_Chat
{
    /*
    Ключевой сущностью в SignalR, через которую клиенты обмениваются сообщеними 
    с сервером и между собой, является хаб (hub). 
    Хаб представляет некоторый класс, который унаследован от абстрактного класса 
    Microsoft.AspNetCore.SignalR.Hub.
    */
    public class ChatHub : Hub
    {
        private MessageContext messageContext;
        public ChatHub(MessageContext message)
        {
            this.messageContext = message;
        }

        static List<User> Users = new List<User>();

        // Отправка сообщений
        public async Task Send(string username, string message)
        {
            var user = messageContext.Users.FirstOrDefault(x => x.Name == username);
            M_UserMessage newMessage = new M_UserMessage()
            {
                Body = message,
                UserName = user?.Name ?? "undefined",
            };
            messageContext.Messages.Add(newMessage);
            messageContext.SaveChanges();

            // Вызов метода AddMessage на всех клиентах
            await Clients.All.SendAsync("AddMessage", username, message);
        }

        // Подключение нового пользователя
        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

            var user = messageContext.Users.FirstOrDefault(x => x.ConnectionId == id);
            if (user == null)
            {
                user = new User { ConnectionId = id, Name = userName, IsLoggedIn = true };

                messageContext.Users.Add(user);
                messageContext.SaveChanges();

                await Groups.AddToGroupAsync(id, "LoggedInUsers");

                var userList = messageContext.Users.Where(x => x.IsLoggedIn).ToList();
                var messageList = messageContext.Messages.ToList();

                await Clients.Caller.SendAsync("Connected", id, userName, userList, messageList);

            }

            // Отправка сообщения только тем, кто в группе "LoggedInUsers"
            await Clients.Group("LoggedInUsers").SendAsync("NewUserConnected", id, userName);

        }

        // OnDisconnectedAsync срабатывает при отключении клиента.
        // В качестве параметра передается сообщение об ошибке, которая описывает,
        // почему произошло отключение.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                messageContext.Users.Remove(item);
                messageContext.SaveChanges();

                Users.Remove(item);
                var id = Context.ConnectionId;
                // Вызов метода UserDisconnected на всех клиентах
                await Clients.All.SendAsync("UserDisconnected", id, item.Name);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
