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

/*
    Почему структура запроса должна быть такой, аргументы:

    ---

    string QueryData.City;

    Город в составе запроса должен быть один т.к. несколько городов в одном запросе будут присутствовать не часто.
    Но такую возможность тоже можно поддержать, "распарсится" на два запроса для разных городов. т.е. Будет просто
    два QueryData для одного InitiatorId.

    ---

    Dictionary<DateTime, WeatherEntities> weatherAtTimes;

    Для конкретного времени либо частей дня (утро, день, вечер, ночь), а так же самих дней, месяцев, годов и пр.
    достаточно переменной DateTime, не нужно никаких лишних структур и типов. В базе могут быть какие угодно структуры,
    в запросе - чем проще и легче (по "весу"), тем лучше и быстрее. Т.к. ими придётся перекидываться между сервисами.

    Если пользователю нужен какой-то временной интервал - этих DateTime может быть несколько, например:
    
    утро, день, вечер, ночь - в DateTime 06:00, 12:00, 18:00, 00:00
    вчера, сегодня, завтра  - в DateTime 27.04.2016 - 12:00, 28.04.2016 - 12:00, 29.04.2016 - 12:00
    
    по такому же принципу заполняются запросы вида: "месяц назад на неделю" или "послезавтра весь день",
    или "в субботу, после обеда" и все остальные.

    WeatherEntities - простая структура со всеми возможными погодными данными в запрошенное время, т.е.
    Dictionary<DateTime, WeatherEntities> weatherAtTimes; ни что иное как список пар вида: ВРЕМЯ и ПОГОДА в это время.
    
    Всё, ничего лишнего )

    - Art.Stea1th
*/