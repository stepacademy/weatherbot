using System;
using System.Linq;
using System.Data.Entity;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using WeatherBot.Database;
using WeatherBot.Database.Entities;
using WeatherBot.DatabaseWorker.QueryComponents;

// Женя, тут необходимо заполнить QueryData погодой одного конкретного City для каждого из WeatherEntities напротив каждого DateTime
// это метод GetWeatherAtCityTime(string city, DateTime dateTime) который по примеру возвращает погоду в какое-то конкретное время, или время суток или день и т.п.
// этим в свою очередь заполняется в цикле в SetResponse(QueryData query) ответ

// p.s. в GetWeatherAtCityTime(string city, DateTime dateTime) знаю, что неправильнно запрашиваю, т.к. не то )

namespace WeatherBot.DatabaseWorker {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal class QueryHandler : IQueryHandlerContract {

        private ICallbackResponseContract _currentOperationContext;
        private WeatherDbContext          _currentWeatherDbContext;

        public async void QueryAsync(QueryData query) {

            // берём контекст текущей операции
            _currentOperationContext = OperationContext.Current.GetCallbackChannel<ICallbackResponseContract>();

            // возвращаем результат, предварительно заполнив в SetResponse ответами наш запрос
            //_currentOperationContext.Response(await SetResponse(query));       // <-- This line will be uncomment

            _currentOperationContext.Response(query);                            // <-- This line will be removed, DUMMY

        }

        private async Task<QueryData> SetResponse(QueryData query) {

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

        private async Task<WeatherData> GetWeatherAtCityTime(string city, DateTime dateTime) {

            int id = 0; // <-- ?

            using (_currentWeatherDbContext = new WeatherDbContext()) {

                _currentWeatherDbContext.WeatherDatas.Load();

                IQueryable<WeatherData> wData =
                    from weatherData in _currentWeatherDbContext.WeatherDatas
                    where weatherData.Id == id // <-- ?
                    select weatherData;

                return await wData.FirstAsync();
            }
        }

    }
}