using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WeatherBot.Database;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    public class Weather : IWeather
    {
        public QDataWeatherDay GetDataWeatherDay(DateTime dataTime)
        {
            throw new NotImplementedException();
        }

        public QDataWeatherDay GetWeatherDay(string city)
        {
            throw new NotImplementedException();
        }

        public void UpdateCities()
        {
            using (var db = new Database.WeatherDbContext())
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
                    var country = new Country()
                    {
                        Name = countryElement.GetAttribute("name"),
                        Cities = new List<City>()
                    };

                    foreach (XmlNode cityNode in countryElement.Cast<XmlNode>().Where(cityNode => cityNode.Attributes != null))
                    {
                        country.Cities.Add(new City()
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

        public void UpdateCityWeather(IEnumerable<City> cities)
        {
            var stackCities = cities as Stack<City>;

            var formatSepar = new NumberFormatInfo { NumberDecimalSeparator = "." };

            while (stackCities != null && stackCities.Count > 0)
            {
                var city = stackCities.Pop();
                var doc = XDocument.Load($"http://export.yandex.ru/weather-ng/forecasts/{city.XmlCode}.xml");

                if (doc.Root == null) continue;

                var ns = doc.Root.GetDefaultNamespace();

                var fact = doc.Root.Element(ns + "fact");

                var factWeather = city.Weather.Fact;
                if (fact == null) continue;

                var observationTimeElement = fact.Element(ns + "observation_time");
                if (observationTimeElement == null) continue;

                var obsDate = Convert.ToDateTime(observationTimeElement.Value);
                if (factWeather.ObservationTime == obsDate) continue;

                factWeather.ObservationTime = obsDate;

                WeatherUpdate.WeatherDataProccessing(formatSepar, ns, fact, factWeather);
            }
        }

#region get data from Db

        public static WindDirectionType GetWindDirectionType(string windDirection)
        {
            WindDirectionType wd;
            Enum.TryParse(windDirection, out wd);

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
#endregion

        private void ForecastUpdate(IEnumerable<City> cities)
        {
            var stackCities = cities as Stack<City>;

            var formatSepar = new NumberFormatInfo { NumberDecimalSeparator = "." };

            while (stackCities != null && stackCities.Count > 0)
            {
                var city = stackCities.Pop();


            }
        }


        //public void UpdateWeather()
        //{

        //    using (var db = new Database.WeatherDbContext())
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

        private static void UpdateLocations(City city, XmlElement root)
        {
            if (city.Location == null)
            {
                city.Location = new Location()
                {
                    City = city,
                    Latitude =
                        Convert.ToDouble(root.Attributes.GetNamedItem("lat").InnerText.Replace('.', ',')),
                    Longitude =
                        Convert.ToDouble(root.Attributes.GetNamedItem("lon").InnerText.Replace('.', ','))
                };
            }

        }

        public void UpdateWeather()
        {
            throw new NotImplementedException();
        }
    }
}
