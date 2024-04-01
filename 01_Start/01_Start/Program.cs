internal class Program
{
    // downloaded: Snippet for HTML https://marketplace.visualstudio.com/items?itemName=MadsKristensen.HTMLSnippetPack
    // downloaded: Code Prettier for C# Code https://marketplace.visualstudio.com/items?itemName=WinstonFeng.VSFormatOnSave2022&ssr=false#overview
    // downloaded: For lorem text/image ZenCoding https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ZenCoding
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();

        app.UseStaticFiles();

        app.Run(async (context) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.SendFileAsync("wwwroot/index.html");
        });

        app.Run();
    }
}