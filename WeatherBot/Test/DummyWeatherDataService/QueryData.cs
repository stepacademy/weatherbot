using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// rational Query-Data organization

namespace Test.DummyWeatherDataService {

    [DataContract]
    public class QueryData {

        [DataMember]
        public string City;

        [DataMember]
        public Dictionary<DateTime, WeatherAtTime> wAtTime;
    }
}