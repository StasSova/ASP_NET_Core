namespace _02_Middleware;
public static class FromTwentyToHundredExtensions
{
    public static IApplicationBuilder UseFromTwentyToHundred(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromTwentyToHundredMiddleware>();
    }
}
public class FromTwentyToHundredMiddleware
{
    private readonly RequestDelegate _next;

    public FromTwentyToHundredMiddleware(RequestDelegate next)
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
            if (number < 20)
            {
                await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
            }
            else
            {
                string[] Tens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                string existingResult = context.Session.GetString("number") ?? "";
                string thousandResult = context.Session.GetString("thousand") ?? "";
                string thousandProcess = context.Session.GetString("thousandProcess") ?? "false";


                switch (number)
                {
                    // 25654
                    case int n when n >= 20_000 && thousandProcess == "true":
                        // 25654 -> 25
                        int num = n / 1000;
                        // 25/10 -> 2.5 -> 2
                        thousandResult += $" {Tens[(int)num / 10 - 2]}";
                        context.Session.SetString("thousand", thousandResult);
                        if (n % 1000 != 0)
                        {
                            await _next.Invoke(context);
                        }
                        break;

                    case int n when n >= 20 && thousandProcess == "false":
                        // 25654 -> 54
                        // 101 
                        if (token.Substring(token.Length - 2, 1) != "0" && token.Substring(token.Length - 2, 1) != "1")
                        {
                            int lastNumber = Convert.ToInt32(token.Substring(token.Length - 2));
                            existingResult += $" {Tens[lastNumber / 10 - 2]}";
                            if (number % 10 == 0)
                            {
                                // Выдаем окончательный ответ клиенту
                                await context.Response.WriteAsync("Your number is " + existingResult);
                                context.Session.Remove("number");
                                return;
                            }
                            else
                            {
                                context.Session.SetString("number", existingResult);
                                await _next.Invoke(context);
                            }
                        }
                        else
                        {
                            await _next.Invoke(context);
                        }
                        break;

                    default:
                        await _next.Invoke(context);
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
