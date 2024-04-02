using _02_Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Все сессии работают поверх объекта IDistributedCache, и 
        // ASP.NET Core предоставляет встроенную реализацию IDistributedCache
        builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
        builder.Services.AddSession();  // Добавляем сервисы сессии
        var app = builder.Build();

        app.UseSession();   // Добавляем middleware-компонент для работы с сессиями
        // Добавляем middleware-компоненты в конвейер обработки запроса.
        app.FromThousandsToMillion(); // 1000 - 999 999
        app.UseFromHundredsToThousands(); // 100 - 999        
        app.UseFromTwentyToHundred();// 20-99
        app.UseFromElevenToNineteen();//11-19
        app.UseFromOneToTen();//1-9

        app.Run();
    }
}