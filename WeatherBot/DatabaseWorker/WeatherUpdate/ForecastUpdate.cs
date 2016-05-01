using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeatherBot.Database;
using WeatherBot.Database.Entities;

namespace WeatherBot.DatabaseWorker.WeatherUpdate
{
    internal class ForecastUpdate : WeatherUpdate
    {
        public override void UpdateCity(IEnumerable<City> cities)
        {
            if (cities == null) return;

            UpdateCityFactWeather(cities);
            DayProcessing(cities);
        }

        private void DayProcessing(IEnumerable<City> cities)
        {
            var stackCities = cities as Stack<City>;

            var formatSepar = new NumberFormatInfo {NumberDecimalSeparator = "."};

            while (stackCities != null && stackCities.Count > 0)
            {
                var city = stackCities.Pop();
                var doc = XDocument.Load($"http://export.yandex.ru/weather-ng/forecasts/{city.XmlCode}.xml");

                if (doc.Root == null) continue;

                var ns = doc.Root.GetDefaultNamespace();

                var daysNodes = doc.Root.Elements(ns + "day");

                List<CalendarDate> calendarDates;
                using (var db = new WeatherDbContext())
                {
                    db.CalendarDates.Load();
                    calendarDates = db.CalendarDates.ToList();
                }

                foreach (var day in daysNodes)
                {
                    CalendarDate calendarDate;
                    try
                    {
                        calendarDate =
                            calendarDates
                                .First(date => date.Date == Convert.ToDateTime(day.Attribute("date").Value));
                    }
                    catch (ArgumentNullException)
                    {
                        var cd = new CalendarDate {Date = Convert.ToDateTime(day.Attribute("date").Value)};
                        calendarDates.Add(cd);
                        calendarDate = cd;
                    }


                    var dayPartsNodes = day.Elements(ns + "day_part");
                    var dayParts = DayPartsProcessing(formatSepar, ns, dayPartsNodes);

                    city.Weather.Forecast.Add(new ForecastWeather
                    {
                        CalendarDate = calendarDate,
                        DayParts = dayParts
                    });
                }
            }
        }

        public static List<DayPart> DayPartsProcessing(NumberFormatInfo formatSepar, XNamespace ns,
            IEnumerable<XElement> dayPartsNodes)
        {
            var dayParts = new List<DayPart>();

            foreach (var dayPartNode in dayPartsNodes)
            {
                var dp = new DayPart {WeatherData = new WeatherData()};

                if (Convert.ToInt32(dayPartNode.Attribute("typeid").Value) < 5)
                    dp.DayTime = DbAction.GetDayTimeType(dayPartNode.Attribute("type").Value);
                else continue;


                WeatherDataProccessing(formatSepar, ns, dayPartNode, dp.WeatherData);

                dayParts.Add(dp);
            }

            return dayParts;
        }

        public static async Task<City> OneCityDayProcessing(City city)
        {
            var formatSepar = new NumberFormatInfo { NumberDecimalSeparator = "." };

            var doc = XDocument.Load($"http://export.yandex.ru/weather-ng/forecasts/{city.XmlCode}.xml");

            if (doc.Root == null) return city;

            var ns = doc.Root.GetDefaultNamespace();

            var daysNodes = doc.Root.Elements(ns + "day");

            foreach (var day in daysNodes)
            {
                var calendarDate = new CalendarDate { Date = Convert.ToDateTime(day.Attribute("date").Value) };

                var dayPartsNodes = day.Elements(ns + "day_part");
                var dayParts = DayPartsProcessing(formatSepar, ns, dayPartsNodes);

                city.Weather.Forecast.Add(new ForecastWeather
                {
                    CalendarDate = calendarDate,
                    DayParts = dayParts
                });
            }

            return city;
        }
    }
}