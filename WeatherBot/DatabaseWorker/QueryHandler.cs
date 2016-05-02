using System;
using System.Linq;
using System.Data.Entity;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherBot.DatabaseWorker {

    using Database;
    using Database.Entities;
    using QueryComponents;
    using WeatherUpdate;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal class QueryHandler : IQueryHandlerContract
    {

        private ICallbackResponseContract _currentOperationContext;
        private WeatherDbContext          _currentWeatherDbContext;

        public async void QueryAsync(QueryData query)
        {

            if (_currentOperationContext == null)
                _currentOperationContext = OperationContext.Current.GetCallbackChannel<ICallbackResponseContract>();

            _currentOperationContext.Response(await Response(query));
        }

        private async Task<QueryData> Response(QueryData query)
        {

            List<DateTime> queryDateTimes = new List<DateTime>(query.WeatherAtTimes.Keys);

            foreach (var dateTime in queryDateTimes)
            {
                // WeatherData wData = await GetWeatherAtCityTime(query.City, dateTime);       // <- will be uncomment
                WeatherData wData = await GetDirectWeatherAtCityTime(query.City, dateTime);    // <- for debug, will be removed
                // WeatherData wData = await GetDummyWeatherAtCityTime(query.City, dateTime);  // <- for debug, will be removed
                
                query.WeatherAtTimes[dateTime].Temperature   = wData.Temperature;
                query.WeatherAtTimes[dateTime].Humidity      = wData.Humidity;
                query.WeatherAtTimes[dateTime].Pressure      = wData.Pressure;
                query.WeatherAtTimes[dateTime].WindDirection = wData.WindDirection;
                query.WeatherAtTimes[dateTime].WindSpeed     = wData.WindSpeed;


            }

            return query;
        }
        
        private async Task<WeatherData> GetWeatherAtCityTime(string fCity, DateTime dateTime)
        {
            
            var queryCity = from city in _currentWeatherDbContext.Cities where city.Name == fCity select city;

            DayTimeType dt = DbAction.GetDayTimeType(dateTime.Hour);

            using (_currentWeatherDbContext = new WeatherDbContext())
            {
                _currentWeatherDbContext.Weathers.Load();
                
                var fWeather =
                    queryCity.First()
                        .Weather.Forecast.First(forecastWeather => forecastWeather.CalendarDate.Date == dateTime.Date);
                
                var wData = fWeather.DayParts.Where(dayPart => dayPart.DayTime == dt)
                                .Select(dayPart => dayPart.WeatherData).First();

                return wData;
            }
        }

        private async Task<WeatherData> GetDirectWeatherAtCityTime(string fCity, DateTime dateTime)
        {
            var countries = await WeatherUpdate.Weather.DownloadCities();

            City qCity = new City(); // <-- null ref hotfix
            bool flag = false;
            foreach (var country in countries)
            {
                foreach (var city in country.Cities)
                {
                    if (city.Name != fCity) continue;
                    qCity = city;
                    flag = true;
                    break;
                }
                if(flag)
                    break;
                
            }

            City resCity = ForecastUpdate.OneCityDayProcessing(qCity);

            WeatherData wData = null;
            
            if (resCity != null)
            {
                DayTimeType dt = DbAction.GetDayTimeType(dateTime.Hour);

                foreach (var forecastWeather in resCity.Weather.Forecast)
                {
                    if (forecastWeather.CalendarDate.Date != dateTime.Date) continue;

                    wData = forecastWeather.DayParts.First(dayPart => dayPart.DayTime == dt).WeatherData;

                    break;
                }
            }

            return wData;
        }

        private async Task<WeatherData> GetDummyWeatherAtCityTime(string fCity, DateTime dateTime) {

            Task<WeatherData> taskWData = new Task<WeatherData>(delegate () {

                WeatherData wData = new WeatherData();
                wData.WeatherState = new WeatherState();

                wData.WeatherState.State = "солнечно";
                wData.Temperature        = 16.1;
                wData.Humidity           = 97;
                wData.Pressure           = 768;
                wData.WindDirection      = WindDirectionType.NorthWest;
                wData.WindSpeed          = 2.9;

                return wData;
            });

            taskWData.Start();
            return await taskWData;
        }
    }
}