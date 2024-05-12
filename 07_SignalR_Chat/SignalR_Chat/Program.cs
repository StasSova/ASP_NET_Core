using Microsoft.EntityFrameworkCore;
using SignalR_Chat;   // пространство имен класса ChatHub

var builder = WebApplication.CreateBuilder(args);
// Для использования функциональности библиотеки SignalR,
// в приложении необходимо зарегистрировать соответствующие сервисы
builder.Services.AddSignalR();

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<MessageContext>(options => options.UseSqlServer(connection));



var app = builder.Build();
// С помощью специального метода расширения UseDefaultFiles() можно настроить
// отправку статических веб-страниц по умолчанию без обращения к ним по полному пути.
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");   // ChatHub будет обрабатывать запросы по пути /chat

app.Run();
