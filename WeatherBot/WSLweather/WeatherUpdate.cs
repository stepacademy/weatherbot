using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    class WeatherUpdate : IWeatherUpdate
    {
        public static void WeatherDataProccessing(NumberFormatInfo formatSepar, XNamespace ns, XElement itemXElement, FactWeather containerWeather)
        {
            var factWeatherData = containerWeather.WeatherData;

            var temperatureElement = itemXElement.Element(ns + "temperature");
            if (temperatureElement != null)
                factWeatherData.Temperature = Convert.ToDouble(temperatureElement.Value, formatSepar);

            var humidityElement = itemXElement.Element(ns + "humidity");
            if (humidityElement != null)
                factWeatherData.Humidity = Convert.ToInt32(humidityElement.Value, formatSepar);

            var pressureElement = itemXElement.Element(ns + "pressure");
            if (pressureElement != null)
                factWeatherData.Pressure = Convert.ToInt32(pressureElement.Value, formatSepar);

            var stateElement = itemXElement.Element(ns + "image-v3");
            if (stateElement != null)
                factWeatherData.WeatherState = Weather.GetWeatherState(stateElement.Value);

            var windDirectionElement = itemXElement.Element(ns + "wind_direction");
            if (windDirectionElement != null)
                factWeatherData.WindDirection = Weather.GetWindDirectionType(windDirectionElement.Value);

            var windSpeedElement = itemXElement.Element(ns + "wind_speed");
            if (windSpeedElement != null)
                factWeatherData.WindSpeed = Convert.ToDouble(windSpeedElement.Value, formatSepar);
        }

        public virtual void UpdateCity(IEnumerable<City> cities)
        {
        }
    }
}