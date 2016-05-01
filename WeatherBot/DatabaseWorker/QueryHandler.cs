using System;
using System.Collections;
using System.Linq;
using System.Data.Entity;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using WeatherBot.Database;
using WeatherBot.Database.Entities;
using WeatherBot.DatabaseWorker.QueryComponents;
using WeatherBot.DatabaseWorker;
using WeatherBot.DatabaseWorker.WeatherUpdate;

namespace WeatherBot.DatabaseWorker {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal class QueryHandler : IQueryHandlerContract {

        private ICallbackResponseContract _currentOperationContext;
        private WeatherDbContext          _currentWeatherDbContext;

        public async void QueryAsync(QueryData query) {                      // <-- Async line will be uncomment

            // берём контекст текущей операции
            _currentOperationContext = OperationContext.Current.GetCallbackChannel<ICallbackResponseContract>();

            // возвращаем результат, предварительно заполнив в SetResponse ответами наш запрос
            _currentOperationContext.Response(await SetResponse(query));       // <-- This line will be uncomment

            //_currentOperationContext.Response(query);                            // <-- This line will be removed, DUMMY

        }

        private async Task<QueryData> SetResponse(QueryData query) {

            List<DateTime> queryDateTimes = new List<DateTime>(query.weatherAtTimes.Keys);

            foreach (DateTime dateTime in queryDateTimes)
            {
                WeatherData wData = await GetWeatherAtCityTime(query.City, dateTime); 

                query.weatherAtTimes[dateTime].State         = wData.WeatherState.State;
                query.weatherAtTimes[dateTime].Temperature   = wData.Temperature;
                query.weatherAtTimes[dateTime].Humidity      = wData.Humidity;
                query.weatherAtTimes[dateTime].Pressure      = wData.Pressure;
                query.weatherAtTimes[dateTime].WindDirection = wData.WindDirection;
                query.weatherAtTimes[dateTime].WindSpeed     = wData.WindSpeed;
            }

            return query;
        }
        
        private async Task<WeatherData> GetWeatherAtCityTime(string fCity, DateTime dateTime) {
            
            var queryCity = from city in _currentWeatherDbContext.Cities where city.Name == fCity select city;

            DayTimeType dt = DbAction.GetDayTimeType(dateTime.Hour);

            using (_currentWeatherDbContext = new WeatherDbContext()) {

                _currentWeatherDbContext.Weathers.Load();
                
                var fWeather =
                    queryCity.First()
                        .Weather.Forecast.First(forecastWeather => forecastWeather.CalendarDate.Date == dateTime.Date);
                
                var wData = fWeather.DayParts.Where(dayPart => dayPart.DayTime == dt)
                                .Select(dayPart => dayPart.WeatherData).First();

                return wData;
            }
        }

        private async Task<WeatherData> SetResponseDirectDummy(string fCity, DateTime dateTime)
        {
            var countries = await WeatherUpdate.Weather.DownloadCities();

            City qCity = null;

            foreach (var country in countries)
            {
                foreach (var city in country.Cities)
                {
                    if (city.Name != fCity) continue;
                    qCity = city;
                    break;
                }
            }

            var resCity = await ForecastUpdate.OneCityDayProcessing(qCity);

            WeatherData wData = null;
            
            if (resCity != null)
            {
                DayTimeType dt = DbAction.GetDayTimeType(dateTime.Hour);

                foreach (var forecastWeather in resCity.Weather.Forecast)
                {
                    if (forecastWeather.CalendarDate.Date != dateTime.Date) continue;

                    wData = forecastWeather.DayParts.First(dayPart => dayPart.DayTime == dt).WeatherData;
                }
            }

            return wData;
        }

        

    }
}