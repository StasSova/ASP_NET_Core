namespace _02_Middleware;
public static class FromElevenToNineteenExtensions
{
    public static IApplicationBuilder UseFromElevenToNineteen(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromElevenToNineteenMiddleware>();
    }
}

public class FromElevenToNineteenMiddleware
{
    private readonly RequestDelegate _next;

    public FromElevenToNineteenMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? token = context.Request.Query["number"];
        if (token == null) return;
        try
        {
            int number = Convert.ToInt32(token);
            number = Math.Abs(number);

            if (number < 11)
            {
                await _next.Invoke(context);
                return;
            }

            string[] Numbers = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string existingResult = context.Session.GetString("number") ?? "";
            string thousandResult = context.Session.GetString("thousand") ?? "";
            string thousandProcess = context.Session.GetString("thousandProcess") ?? "false";

            int lastNumber = Convert.ToInt32(token.Substring(token.Length - 2));

            // 1011

            switch (number)
            {
                // 11000 - 19000 - 15860
                case int n when number >= 11_000 && number <= 19_000 && thousandProcess == "true":
                    // 15860 -> 15
                    int two = (int)n / 1000;
                    thousandResult += $" {Numbers[two - 11]}";
                    context.Session.SetString("thousand", thousandResult);
                    break;

                // 11 - 19
                case int n when lastNumber >= 11 && lastNumber <= 19 && thousandProcess == "false":
                    existingResult += $" {Numbers[lastNumber - 11]}";
                    await context.Response.WriteAsync("Your number is " + existingResult);
                    context.Session.Remove("number");
                    break;

                default:
                    await _next.Invoke(context);
                    break;
            }
        }
        catch (Exception)
        {
            // Выдаем окончательный ответ клиенту
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}
