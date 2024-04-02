import { USER_KEY } from "./Constants";

export default class WeatherService {
    static async getCurrentWeather(city) {
        try {
            const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${USER_KEY}&units=metric`;
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error("Failed to fetch current weather data.");
            }
            const data = await response.json();
            return data;
        } catch (ex) {
            console.error(ex);
            return null;
        }
    }

    static async getHourlyForecast(city) {
        try {
            const url = `https://api.openweathermap.org/data/2.5/forecast?q=${city}&appid=${USER_KEY}&units=metric`;
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error("Failed to fetch hourly forecast data.");
            }
            const data = await response.json();
            return data;
        } catch (ex) {
            console.error(ex);
            return null;
        }
    }
}