using System;
using System.Linq;
using System.Data.Entity;
using System.ServiceModel;
using System.Collections.Generic;

using WeatherBot.Database;
using WeatherBot.Database.Entities;
using WeatherBot.DatabaseWorker.QueryComponents;

namespace WeatherBot.DatabaseWorker {

    internal class QueryHandler : IQueryHandlerContract {

        private ICallbackResponseContract _currentOperationContext;
        private WeatherDbContext          _currentWeatherDbContext;

        public void Query(QueryData query) {

            _currentOperationContext = OperationContext.Current.GetCallbackChannel<ICallbackResponseContract>();
            _currentOperationContext.Response(GetResponse(query));
        }


        // Filling QueryData
        private QueryData GetResponse(QueryData query) {

            List<DateTime> queryDateTimes = new List<DateTime>(query.weatherAtTimes.Keys);

            for (int i = 0; i < queryDateTimes.Count; ++i) {

                query.weatherAtTimes[queryDateTimes[i]].Temperature   = 123; // get..
                query.weatherAtTimes[queryDateTimes[i]].Humidity      = 123;
                query.weatherAtTimes[queryDateTimes[i]].Pressure      = 123;
                query.weatherAtTimes[queryDateTimes[i]].WindDirection = WindDirectionType.South;
                query.weatherAtTimes[queryDateTimes[i]].WindSpeed     = 123;

                //KeyValuePair a = new KeyValuePair<int, int>
            }

            return query;
        }




        private WeatherState GetWeatherState(string stateCode) {

            WeatherState result;
            using (var db = new WeatherDbContext()) {
                db.WeatherStates.Load();
                var query = from wst in db.WeatherStates where wst.Code == stateCode select wst;

                result = query.First();
            }

            return result;
        }
    }
}