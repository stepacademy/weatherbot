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
                    var country = new Country()
                    {
                        Name = countryElement.GetAttribute("name"),
                        Cities = new List<City>()
                    };

                    foreach (XmlNode cityNode in countryElement)
                    {
                        if (cityNode.Attributes != null)
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


        public void UpdateWeather()
        {
            using (var db = new WeatherDbContext())
            {
                db.Cities.Load();

                var lstCities = db.Cities.ToList();

                if (lstCities.Count == 0) return;

                var doc = new XmlDocument();

                foreach (var city in lstCities)
                {
                    doc.Load($"http://export.yandex.ru/weather-ng/forecasts/{city.XmlCode}.xml");


                }

            }
        }
    }
}
