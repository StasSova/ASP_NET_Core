namespace RequestProcessingPipeline
{
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
                    string[] Ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                    // Любые числа больше 20, но не кратные 10
                    switch (number)
                    {
                        case int n when n > 100000:


                            break;
                        case int n when n > 10000:


                            break;
                        case int n when n > 1000:


                            break;
                        case int n when n > 100:


                            break;
                        case int n when n > 20:
                            // Записываем в сессионную переменную number результат для компонента FromTwentyToHundredMiddleware
                            context.Session.SetString("number", Ones[number % 10 - 1]);
                            break;
                        default:
                            await context.Response.WriteAsync("Your number is " + Ones[number - 1]); // от 1 до 9
                            // Ваш код для других случаев, если нужно
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
}
