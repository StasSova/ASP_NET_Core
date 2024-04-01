
$(document).ready(async function () {
    const { WeatherService } = await import("./API");

    // Указатели на переменные
    const cur_sect = $("#cur_day_sect");
    const horly_sect = $("#horly_sect");
    const error_sect = $("#error_sect");
    const cityTitle = $("#cur_day_sect .sect-title p").first();
    const date = $("#cur_day_sect .sect-title p").last();
    const weatherIcon = $("#cur_day_sect .icon-block img");
    const temperature = $("#temp-value");
    const minTemperature = $("#min-temp");
    const maxTemperature = $("#max-temp");
    const windSpeed = $("#wind-speed");
    const hourlySection = $("#horly_sect .items");

    // Скрываем все элементы
    cur_sect.hide();
    horly_sect.hide();
    error_sect.hide();

    // Метод для добавления нового элемента
    function addWeatherItem(time, icon, description, temperature, windSpeed) {
        var newItem = $('<div class="weather-item">' +
            '<p class="sec-text">' + time + '</p>' +
            '<img src="' + icon + '" alt="' + description + '" />' +
            '<p>' + description + '</p>' +
            '<p>' + temperature + '</p>' +
            '<p>' + windSpeed + '</p>' +
            '</div>');
        hourlySection.append(newItem);
    }

    // Обработчик события для кнопки поиска погоды
    $("#search_btn").on("click", async function () {
        var city = $("#search input").val();
        if (city) {
            var weatherData = await WeatherService.search(city);

            if (weatherData) {
                // Очищаем предыдущие данные о погоде
                hourlySection.empty();
                // Показываем секции с информацией о погоде
                cur_sect.show();
                horly_sect.show();
                error_sect.hide(); // Скрываем блок с ошибкой
                // Добавляем элементы погоды в коллекцию
                weatherData.forEach(weatherItem => {
                    addWeatherItem(
                        weatherItem.time,
                        weatherItem.icon,
                        weatherItem.description,
                        weatherItem.temperature,
                        weatherItem.windSpeed
                    );
                });
            } else {
                // Показываем блок с ошибкой
                error_sect.show();
            }
        } else {
            // Показываем блок с ошибкой, если поле ввода пустое
            error_sect.show();
        }
    });
});