using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace Test.DummyWeatherDataService {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class WeatherDataService : IWeatherDataService {

        public void SendToDatabaseQuery(QueryData q) {

            if (q.City == null || q.City == "")
                return;
        }
    }

    internal class HARDCODE_DummyDatabase {

        private QueryData _qd;

        public HARDCODE_DummyDatabase() {

            _qd = new QueryData();
            _qd.City = null;

            _qd.wAtTime = new Dictionary<DateTime, WeatherAtTime>();

            foreach (var item in _qd.wAtTime) {  // req. on datetimes from database
                item.Value.Temperature = 10.0;
                item.Value.WindSpeed = 100500;
                item.Value.Pressure = 765;
                item.Value.WeatherState = "sun";
                item.Value.WindDirection = "<--";
                // ...
            }
        }
    }
}