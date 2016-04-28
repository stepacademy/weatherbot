using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    internal class WeatherUpdate : IWeatherUpdate
    {
        public virtual void UpdateCity(IEnumerable<City> cities)
        {
        }

        public void UpdateCityFactWeather(IEnumerable<City> cities)
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

                WeatherDataProccessing(formatSepar, ns, fact, factWeather.WeatherData);
            }
        }

        public static void WeatherDataProccessing(NumberFormatInfo formatSepar, XNamespace ns, XElement itemXElement,
            WeatherData containerWeather)
        {
            var itemWeatherData = containerWeather;

            var temperatureElement = itemXElement.Element(ns + "temperature");
            if (temperatureElement != null)
                itemWeatherData.Temperature = Convert.ToDouble(temperatureElement.Value, formatSepar);
            else
            {
                var temperatureFromElement = itemXElement.Element(ns + "temperature_from");
                var temperatureToElement = itemXElement.Element(ns + "temperature_to");
                if (temperatureFromElement != null && temperatureToElement != null)
                    itemWeatherData.Temperature = Convert.ToDouble(temperatureFromElement.Value, formatSepar) +
                                                  Convert.ToDouble(temperatureToElement.Value, formatSepar)/2;
            }

            var humidityElement = itemXElement.Element(ns + "humidity");
            if (humidityElement != null)
                itemWeatherData.Humidity = Convert.ToInt32(humidityElement.Value, formatSepar);

            var pressureElement = itemXElement.Element(ns + "pressure");
            if (pressureElement != null)
                itemWeatherData.Pressure = Convert.ToInt32(pressureElement.Value, formatSepar);

            var stateElement = itemXElement.Element(ns + "image-v3");
            if (stateElement != null)
                itemWeatherData.WeatherState = Weather.GetWeatherState(stateElement.Value);

            var windDirectionElement = itemXElement.Element(ns + "wind_direction");
            if (windDirectionElement != null)
                itemWeatherData.WindDirection = Weather.GetWindDirectionType(windDirectionElement.Value);

            var windSpeedElement = itemXElement.Element(ns + "wind_speed");
            if (windSpeedElement != null)
                itemWeatherData.WindSpeed = Convert.ToDouble(windSpeedElement.Value, formatSepar);
        }
    }
}