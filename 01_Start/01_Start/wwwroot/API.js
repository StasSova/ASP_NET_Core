import { USER_KEY } from "./Constants";

export default class WeatherService {
    static async search(city) {
        try {
            const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${USER_KEY}&units=metric`;
            const response = await fetch(url);
            const data = await response.json();
            if (response.ok) {
                return data;
            } else {
                throw new Error("Failed to fetch weather data.");
            }
        } catch (ex) {
            console.error(ex);
            return null;
        }
    }
}