using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using WeatherBot.Database;
using WeatherBot.Database.Entities;

namespace WeatherBot.DatabaseWorker.WeatherUpdate
{
    public class Weather : IWeather
    {
        public void UpdateWeather()
        {
            throw new NotImplementedException();
        }
        
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
    }
}