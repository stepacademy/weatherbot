using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using WeatherBot.DatabaseWorker.Database.Entities;

namespace WeatherBot.WSLweather
{
    internal class WeatherUpdate : IWeatherUpdate
    {
        public virtual void UpdateCity(IEnumerable<City> cities)
        {
        }

        public void UpdateCityWeather(IEnumerable<City> cities)
        {
            var stackCities = cities as Stack<City>;

            var formatSepar = new NumberFormatInfo {NumberDecimalSeparator = "."};

            while (stackCities != null && stackCities.Count > 0)
            {
                var city = stackCities.Pop();
                var doc = XDocument.Load($"http://export.yandex.ru/weather-ng/forecasts/{city.XmlCode}.xml");

                if (doc.Root == null) continue;

                var ns = doc.Root.GetDefaultNamespace();

                var fact = doc.Root.Element(ns + "fact");

                var factWeather = city.Weather.Fact;
                if (fact == null) continue;

                var observationTimeElement = fact.Element(ns + "observation_time");
                if (observationTimeElement == null) continue;

                var obsDate = Convert.ToDateTime(observationTimeElement.Value);
                if (factWeather.ObservationTime == obsDate) continue;

                factWeather.ObservationTime = obsDate;

                // WeatherDataProccessing(formatSepar, ns, fact, factWeather);
            }
        }

        public static void WeatherDataProccessing(NumberFormatInfo formatSepar, XNamespace ns, XElement itemXElement,
            FactWeather containerWeather)
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
    }
}