using _06_MusicCollection.Services.Language;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace _06_MusicCollection.Attributes
{
    public class CultureAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        // Фильтр действий, который будет срабатывать при обращении к действиям контроллера и производить локализацию.
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string? cultureName = null;
            // Получаем куки из контекста, которые могут содержать установленную культуру
            var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie;
            else
                cultureName = "en";

            // Список культур
            List<string> cultures = filterContext.HttpContext.RequestServices.GetRequiredService<ILangRead>()
                                    .languageList().Select(t => t.ShortName).ToList()!;
            if (!cultures.Contains(cultureName))
            {
                cultureName = "en";
            }
            // CultureInfo.CreateSpecificCulture создает объект CultureInfo, 
            // который представляет определенный язык и региональные параметры, 
            // соответствующие заданному имени. Функциональность этого объекта зависит от культурного контекста,
            // например, форматирование дат, времени, чисел, валюты, работа с календарем. 

            // При запуске приложения каждый поток в .NET определяет два объекта типа CultureInfo:
            // CurrentCulture - текущую языковую культуру
            // CultureInfo.CurrentUICulture - языковую культуру для пользовательского интерфейса.
            // ASP.NET Core использует эти свойства для рендеринга значений, которые зависят от настройки культуры.
            // Например, в зависимости от культуры может меняться отображение даты и времени.

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            // После этого для локализации система будет выбирать нужный файл ресурсов.
        }
    }
}
