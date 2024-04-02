namespace _02_Middleware;

public static class FromOneToTenExtensions
{
    public static IApplicationBuilder UseFromOneToTen(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromOneToTenMiddleware>();
    }
}

public class FromOneToTenMiddleware
{
    private readonly RequestDelegate _next;

    public FromOneToTenMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? token = context.Request.Query["number"]; // Получим число из контекста запроса
        try
        {
            int number = Convert.ToInt32(token);
            number = Math.Abs(number);
            if (number == 10)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Your number is ten");
            }
            else
            {
                string[] Ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };

                string existingResult = context.Session.GetString("number") ?? "";
                string thousandResult = context.Session.GetString("thousand") ?? "";
                string thousandProcess = context.Session.GetString("thousandProcess") ?? "true";

                // Любые числа больше 20, но не кратные 10
                switch (number)
                {
                    // 25860
                    case int n when n >= 20000 && thousandProcess == "true":
                        // 25860 -> 25
                        int two = (int)n / 1000;
                        // 25 -> 5
                        two = Convert.ToInt16(two.ToString().Substring(1));
                        thousandResult += $" {Ones[two - 1]}";
                        context.Session.SetString("thousand", thousandResult);
                        break;

                    // 11_000 - 19_000 сюда не зайдет...

                    // 6421
                    case int n when n >= 1000 && thousandProcess == "true":
                        context.Session.SetString("thousand", $" {Ones[(int)n / 1000 - 1]}");
                        break;

                    case int n when n > 20:
                        string str = token.Substring(token.Length - 1);
                        int lastNumber = Convert.ToInt16(str);
                        if (lastNumber != 0)
                        {
                            existingResult += $" {Ones[lastNumber - 1]}";
                        }
                        else
                        {
                            existingResult += $" ten";
                        }
                        await context.Response.WriteAsync("Your number is " + existingResult);

                        context.Session.Remove("number");
                        break;

                    default:
                        existingResult += $" {Ones[number - 1]}";
                        await context.Response.WriteAsync("Your number is " + existingResult);
                        context.Session.Remove("number");
                        break;
                }
            }
        }
        catch (Exception)
        {
            // Выдаем окончательный ответ клиенту
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}