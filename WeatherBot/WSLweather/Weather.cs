using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml;
using WeatherBot.Database;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    public class Weather : IWeather
    {

        public void UpdateWeather()
        {
            throw new NotImplementedException();
        }

        //    using (var db = new Database.WeatherDbContext())
        //{

        //public void UpdateWeather()

        #region first DB init

        public void CitiesInit()
        {
            using (var db = new WeatherDbContext())
            {
                var doc = new XmlDocument();
                doc.Load(@"https://pogoda.yandex.ru/static/cities.xml");


                //<cities>
                //    <country name="Абхазия">
                //        <city id="37188" region="27028" head="" type="3" country="Абхазия" part="" resort="" climate="">Новый Афон</city>
                //        <city id="37178" region="10282" head="" type="3" country="Абхазия" part="" resort="" climate="">Пицунда</city>
                //        <city id="37187" region="37187" head="" type="3" country="Абхазия" part="" resort="" climate="">Гудаута</city>
                //        <city id="37172" region="10280" head="" type="3" country="Абхазия" part="" resort="" climate="">Гагра</city>
                //        <city id="37189" region="10281" head="0" type="3" country="Абхазия" part="" resort="0" climate="">Сухум</city>
                //    </country>


                if (doc.DocumentElement == null) return;
                foreach (XmlElement countryElement in doc.DocumentElement)
                {
                    var country = new Country
                    {
                        Name = countryElement.GetAttribute("name"),
                        Cities = new List<City>()
                    };

                    foreach (
                        var cityNode in countryElement.Cast<XmlNode>().Where(cityNode => cityNode.Attributes != null))
                    {
                        country.Cities.Add(new City
                        {
                            Country = country,
                            Name = cityNode.InnerText,
                            XmlCode = cityNode.Attributes.GetNamedItem("id").InnerText
                        });
                    }

                    db.Countries.Add(country);
                }

                db.SaveChanges();
            }
        }

        public void LocationsInit(City city, XmlElement root)
        {
            if (city.Location == null)
            {
                city.Location = new Location
                {
                    City = city,
                    Latitude =
                        Convert.ToDouble(root.Attributes.GetNamedItem("lat").InnerText.Replace('.', ',')),
                    Longitude =
                        Convert.ToDouble(root.Attributes.GetNamedItem("lon").InnerText.Replace('.', ','))
                };
            }
        }

        #endregion

        #region get data from Db

        public static WindDirectionType GetWindDirectionType(string windDirection)
        {
            WindDirectionType wd = WindDirectionType.East; //not null

            switch (windDirection)
            {
                case "s": wd = WindDirectionType.South;
                    break;
                case "w": wd = WindDirectionType.West;
                    break;
                case "e": wd = WindDirectionType.East;
                    break;
                case "n": wd = WindDirectionType.North;
                    break;
                case "se": wd = WindDirectionType.SouthEast;
                    break;
                case "sw": wd = WindDirectionType.SouthWest;
                    break;
                case "ne": wd = WindDirectionType.NorthEast;
                    break;
                case "nw": wd = WindDirectionType.NorthWest;
                    break;
            }

            return wd;
        }

        public static WeatherState GetWeatherState(string stateCode)
        {
            WeatherState result;
            using (var db = new WeatherDbContext())
            {
                db.WeatherStates.Load();
                var query = from wst in db.WeatherStates where wst.Code == stateCode select wst;

                result = query.First();
            }

            return result;
        }

        public static DayTimeType GetDayTimeType(string dayTime)
        {
            DayTimeType result;
            Enum.TryParse(dayTime, out result);

            return result;
        }

        public static DayTimeType GetDayTimeType(int hour)
        {
            DayTimeType result;

            switch (hour)
            {
                case 6: result = DayTimeType.morning;
                    break;
                case 12:
                    result = DayTimeType.day;
                    break;
                case 18:
                    result = DayTimeType.evening;
                    break;
                case 0:
                    result = DayTimeType.night;
                    break;

                default: result = DayTimeType.morning;
                    break;
            }

            return result;
        }



        #endregion

        //    {
        //        db.Cities.Load();

        //        var lstCities = db.Cities.ToList();

        //        if (lstCities.Count == 0) return;


        //        foreach (var city in lstCities)
        //        {
        //            try
        //            {
        //                UpdateLocations(city, root);

        //                if (city.Weather == null)
        //                    city.Weather = new Database.Entities.Weather();

        //                if (city.Weather.Fact == null)
        //                    city.Weather.Fact = new FactWeather() { WeatherData = new WeatherData() };


        //                foreach (XmlNode item in root.ChildNodes)
        //                {
        //                    switch (item.Name)
        //                    {
        //                        case "fact":


        //                            break;
        //                        case "day":
        //                            break;
        //                    }
        //                    if (item.Name == "fact")
        //                    {
        //                        Console.WriteLine(item.InnerText);
        //                    }
        //                    else
        //                {
        //                    throw new ArgumentNullException();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                db.UpdateErrors.Add(new UpdateError()
        //                {
        //                    City = city,
        //                    Exception = ex.Message,
        //                    DateTime = DateTime.Now
        //                });
        //            }

        //        }

        //        db.SaveChanges();
        //    }
        //}
    }
}