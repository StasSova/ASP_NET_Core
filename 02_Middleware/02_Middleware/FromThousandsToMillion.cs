namespace _02_Middleware;


public static class FromThousandsToMillionExtensions
{
    public static IApplicationBuilder FromThousandsToMillion(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromThousandsToMillionMiddleware>();
    }
}
public class FromThousandsToMillionMiddleware
{
    private readonly RequestDelegate _next;

    public FromThousandsToMillionMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? token = context.Request.Query["number"];
        if (token == null) return;
        try
        {
            context.Session.Remove("number");
            context.Session.Remove("thousandProcess");
            context.Session.Remove("thousand");

            int number = Convert.ToInt32(token);
            number = Math.Abs(number);
            if (number < 1000)
            {
                await _next.Invoke(context); // Передаем контекст запроса следующему компоненту
            }
            else
            {
                context.Session.SetString("thousandProcess", "true");
                // получаем текущее значение суммы
                string existingResult = context.Session.GetString("number") ?? "";
                // хотим забрать сколько сотен у нас есть...
                await _next.Invoke(context);
                // там должна была инициализироваться переменная и мы ее забираем
                string thousandResult = context.Session.GetString("thousand") ?? "";
                // добавляем к результату
                existingResult += $"{thousandResult} thousand";
                // отменяем процесс для дальнейшей обработки тысячных
                context.Session.SetString("thousandProcess", "false");
                // если число целое, тогда завершаем
                if (number % 1000 == 0)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is " + existingResult);
                    context.Session.Remove("number");
                    context.Session.Remove("thousandProcess");
                    context.Session.Remove("thousand");


                }
                else
                {
                    context.Session.SetString("number", existingResult);
                    await _next.Invoke(context);
                }
            }
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}