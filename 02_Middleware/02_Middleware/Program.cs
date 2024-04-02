using _02_Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ��� ������ �������� ������ ������� IDistributedCache, � 
        // ASP.NET Core ������������� ���������� ���������� IDistributedCache
        builder.Services.AddDistributedMemoryCache();// ��������� IDistributedMemoryCache
        builder.Services.AddSession();  // ��������� ������� ������
        var app = builder.Build();

        app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������
        // ��������� middleware-���������� � �������� ��������� �������.
        app.FromThousandsToMillion(); // 1000 - 999 999
        app.UseFromHundredsToThousands(); // 100 - 999        
        app.UseFromTwentyToHundred();// 20-99
        app.UseFromElevenToNineteen();//11-19
        app.UseFromOneToTen();//1-9

        app.Run();
    }
}