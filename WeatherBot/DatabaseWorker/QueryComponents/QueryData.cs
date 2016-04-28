using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeatherBot.DatabaseWorker.QueryComponents {

    [DataContract]
    public class QueryData {

        [DataMember]
        public int InitiatorId;

        [DataMember]
        public string City;

        [DataMember]
        public Dictionary<DateTime, WeatherEntities> weatherAtTimes;

    }
}