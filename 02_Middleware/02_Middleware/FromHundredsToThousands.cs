namespace _02_Middleware;

public static class FromHundredsToThousandsExtensions
{
    public static IApplicationBuilder UseFromHundredsToThousands(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromHundredsToThousandsMiddleware>();
    }
}
public class FromHundredsToThousandsMiddleware
{
    private readonly RequestDelegate _next;

    public FromHundredsToThousandsMiddleware(RequestDelegate next)
    {
        this._next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        string? token = context.Request.Query["number"];
        try
        {
            int number = Convert.ToInt32(token);
            number = Math.Abs(number);
            if (number < 100)
            {
                await _next.Invoke(context); // Передаем контекст запроса следующему компоненту
            }
            // 100 - 999
            else
            {
                string[] Hundreds = { "one hundred", "two hundred", "three hundred", "four hundred", "five hundred", "six hundred", "seven hundred", "eight hundred", "nine hundred" };
                string existingResult = context.Session.GetString("number") ?? "";
                string thousandResult = context.Session.GetString("thousand") ?? "";
                string thousandProcess = context.Session.GetString("thousandProcess") ?? "false";


                switch (number)
                {
                    case int n when n >= 100000 && thousandProcess == "true":
                        // 254 654 -> 254

                        // 1000 -> 1
                        int num = n / 1000;
                        // 254/10 -> 25.4 -> 25
                        thousandResult += $" {Hundreds[(int)num - 1]}";
                        thousandResult = thousandResult.Replace("hundred", "");
                        context.Session.SetString("thousand", thousandResult);
                        if (num < 10)
                            break;
                        else if (num % 100 != 0)
                        {
                            await _next.Invoke(context);
                        }
                        break;


                    case int n when n >= 100 && thousandProcess == "false":
                        // 123476 -> 476
                        string str = token.Substring(token.Length - 3);
                        if (str.Substring(0, 1) == "0")
                        {
                            await _next.Invoke(context);
                        }
                        else
                        {
                            int lastNumber = Convert.ToInt32(token.Substring(token.Length - 3));
                            existingResult += $" {Hundreds[(int)lastNumber / 100 - 1]}";
                            if (lastNumber % 100 == 0)
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

                        break;

                    default:
                        await _next.Invoke(context);
                        break;
                }
            }
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}
