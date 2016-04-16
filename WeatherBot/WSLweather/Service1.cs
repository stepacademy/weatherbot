using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Weatherbot.WSLweather;

namespace WSLweather
{
    public class MyClass : IWeatherDbQuery
    {
        public QDataWeatherDay GetWeatherDay(DateTime dataTime)
        {
            throw new NotImplementedException();
        }

        public QDataWeatherDay GetWeatherCityDataWeatherDay(string city)
        {
            throw new NotImplementedException();
        }
    }
}
