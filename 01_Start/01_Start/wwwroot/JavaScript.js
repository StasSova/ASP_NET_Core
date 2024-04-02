import { WeatherService } from "./API";

$(document).ready(async function () {
    const iconUrls = {
        "01d": "https://openweathermap.org/img/wn/01d.png",
        "01n": "https://openweathermap.org/img/wn/01n.png",
        "02d": "https://openweathermap.org/img/wn/02d.png",
        "02n": "https://openweathermap.org/img/wn/02n.png",
        "03d": "https://openweathermap.org/img/wn/03d.png",
        "03n": "https://openweathermap.org/img/wn/03n.png",
        "04d": "https://openweathermap.org/img/wn/04d.png",
        "04n": "https://openweathermap.org/img/wn/04n.png",
        "09d": "https://openweathermap.org/img/wn/09d.png",
        "09n": "https://openweathermap.org/img/wn/09n.png",
        "10d": "https://openweathermap.org/img/wn/10d.png",
        "10n": "https://openweathermap.org/img/wn/10n.png",
        "11d": "https://openweathermap.org/img/wn/11d.png",
        "11n": "https://openweathermap.org/img/wn/11n.png",
        "13d": "https://openweathermap.org/img/wn/13d.png",
        "13n": "https://openweathermap.org/img/wn/13n.png",
        "50d": "https://openweathermap.org/img/wn/50d.png",
        "50n": "https://openweathermap.org/img/wn/50n.png"
    };

    // Указатели на переменные
    const cur_sect = $("#cur_day_sect");
    const horly_sect = $("#horly_sect");
    const error_sect = $("#error_sect");

    const cityTitle = $("#cur_day_sect .sect-title p").first();
    const dateTitle = $("#cur_day_sect .sect-title p").last();
    const date = $("#cur_day_sect .sect-title p").last();
    const weatherDescr = $("#cur_day_sect .icon-block p");
    const weatherIcon = $("#cur_day_sect .icon-block img");
    const temperature = $("#temp-value");
    const minTemperature = $("#min-temp");
    const maxTemperature = $("#max-temp");
    const windSpeed = $("#wind-speed");
    const hourlySection = $("#horly_sect .items");

    const weekday = $("#weekday");

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
            var weatherData = await WeatherService.getCurrentWeather(city);
            if (weatherData) {
                const weatherObject = {
                    cityName: weatherData.name,
                    timestamp: new Date(weatherData.dt * 1000).toLocaleDateString(),
                    currentTemp: weatherData.main.temp,
                    minTemp: weatherData.main.temp_min,
                    maxTemp: weatherData.main.temp_max,
                    weatherIcon: weatherData.weather[0].icon,
                    weatherDescr: weatherData.weather[0].main,
                    windSpeed: weatherData.wind.speed
                };
                dateTitle.text(weatherObject.timestamp);
                cityTitle.text(weatherObject.cityName);
                weatherDescr.text(weatherObject.weatherDescr);
                weatherIcon.attr("src", iconUrls[weatherObject.weatherIcon]);
                temperature.text(weatherObject.currentTemp);
                minTemperature.text(weatherObject.minTemp);
                maxTemperature.text(weatherObject.maxTemp);
                windSpeed.text(weatherObject.windSpeed);


                const currentDate = new Date();
                const options = { weekday: 'long' };
                const formattedDate = currentDate.toLocaleDateString('en-US', options);
                weekday.text(formattedDate);
                cur_sect.show();
                error_sect.hide(); // Скрываем блок с ошибкой


                var hourlyData = await WeatherService.getHourlyForecast(city);
                if (hourlyData) {
                    console.log(hourlyData);
                    // Очищаем предыдущие данные о погоде
                    hourlySection.empty();
                    // Показываем секции с информацией о погоде
                    horly_sect.show();

                    // Добавляем элементы погоды в коллекцию
                    for (let i = 0, j = 0; j < 5; i += 1, j++) {
                        const weatherItem = hourlyData.list[i];

                        const time = new Date(weatherItem.dt * 1000).toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit' });
                        const icon = iconUrls[weatherItem.weather[0].icon];
                        const description = weatherItem.weather[0].description;
                        const temperature = weatherItem.main.temp;
                        const windSpeed = weatherItem.wind.speed;

                        addWeatherItem(time, icon, description, temperature, windSpeed);
                    }
                }



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