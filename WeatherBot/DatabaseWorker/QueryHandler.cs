using System;
using System.Linq;
using System.Data.Entity;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using WeatherBot.Database;
using WeatherBot.Database.Entities;
using WeatherBot.DatabaseWorker.QueryComponents;

namespace WeatherBot.DatabaseWorker {

    internal class QueryHandler : IQueryHandlerContract {

        private ICallbackResponseContract _currentOperationContext;
        private WeatherDbContext          _currentWeatherDbContext;

        public async void QueryAsync(QueryData query) {

            _currentOperationContext = OperationContext.Current.GetCallbackChannel<ICallbackResponseContract>();
            _currentOperationContext.Response(await GetResponse(query));
        }

        private async Task<QueryData> GetResponse(QueryData query) {

            List<DateTime> queryDateTimes = new List<DateTime>(query.weatherAtTimes.Keys);

            for (int i = 0; i < queryDateTimes.Count; ++i) {

                WeatherData wData = await GetWeatherAtCityTime(query.City, queryDateTimes[i]); 

                query.weatherAtTimes[queryDateTimes[i]].State         = wData.WeatherState.State;
                query.weatherAtTimes[queryDateTimes[i]].Temperature   = wData.Temperature;
                query.weatherAtTimes[queryDateTimes[i]].Humidity      = wData.Humidity;
                query.weatherAtTimes[queryDateTimes[i]].Pressure      = wData.Pressure;
                query.weatherAtTimes[queryDateTimes[i]].WindDirection = wData.WindDirection;
                query.weatherAtTimes[queryDateTimes[i]].WindSpeed     = wData.WindSpeed;

            }

            return query;
        }

        private Task<WeatherData> GetWeatherAtCityTime(string city, DateTime dateTime) {

            int id = 0; // <-- ?????????

            using (_currentWeatherDbContext = new WeatherDbContext()) {

                _currentWeatherDbContext.WeatherDatas.Load();

                IQueryable<WeatherData> wData =
                    from weatherData in _currentWeatherDbContext.WeatherDatas
                    where weatherData.Id == id
                    select weatherData;

                return wData.FirstAsync();
            }
        }
    }
}