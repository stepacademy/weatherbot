using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using WeatherBot.Database;
using WeatherBot.Database.Entities;

namespace WeatherBot.DatabaseWorker.WeatherUpdate
{
    public class Weather
    {

        //implement db seed

        #region first DB init

        public async void CitiesInit()
        {
            var lstCities = await DownloadCities();

            if(lstCities == null) return;

            using (var db = new WeatherDbContext())
            {
                foreach (var country in lstCities)
                {
                    db.Countries.Add(country);
                }
                

                db.SaveChanges();
            }
        }

        public static async Task<List<Country>> DownloadCities()
        {
            var lstCountries = new List<Country>();

            var doc = new XmlDocument();
                doc.Load(@"https://pogoda.yandex.ru/static/cities.xml");

            //<cities>
            //      <country name="Абхазия">
            //          ...
            //          ...
            //      </country>
            //      <country name="Австралия">
            //          ...
            //          ...
            //      </country>
            //</cities>


            if (doc.DocumentElement == null) return null;

            foreach (XmlElement countryElement in doc.DocumentElement)
            {
                var country = await ParseCountry(countryElement);
                lstCountries.Add(country);
            }

            return lstCountries;
        }

        private static async Task<Country> ParseCountry(XmlElement countryElement)
        {
            //    <country name="Абхазия">
            //          <city id="37188" region="27028" head="" type="3" country="Абхазия" part="" resort="" climate="">Новый Афон</city>
            //          <city id="37178" region="10282" head="" type="3" country="Абхазия" part="" resort="" climate="">Пицунда</city>
            //          <city id="37187" region="37187" head="" type="3" country="Абхазия" part="" resort="" climate="">Гудаута</city>
            //          <city id="37172" region="10280" head="" type="3" country="Абхазия" part="" resort="" climate="">Гагра</city>
            //          <city id="37189" region="10281" head="0" type="3" country="Абхазия" part="" resort="0" climate="">Сухум</city>
            //    </country>
            
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

            return country;
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