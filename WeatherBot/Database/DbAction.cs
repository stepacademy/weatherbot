using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WeatherBot.Database.Entities;

namespace WeatherBot.Database
{
    public static class DbAction
    {
        #region first DB init

        public static async void CitiesInit()
        {
            var lstCities = await DownloadCities();

            if (lstCities == null) return;

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

        public static void LocationsInit(City city, XmlElement root)
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


        public static WindDirectionType GetWindDirectionType(string windDirection)
        {
            WindDirectionType wd;

            switch (windDirection)
            {
                case "s":
                    wd = WindDirectionType.South;
                    break;
                case "w":
                    wd = WindDirectionType.West;
                    break;
                case "e":
                    wd = WindDirectionType.East;
                    break;
                case "n":
                    wd = WindDirectionType.North;
                    break;
                case "se":
                    wd = WindDirectionType.SouthEast;
                    break;
                case "sw":
                    wd = WindDirectionType.SouthWest;
                    break;
                case "ne":
                    wd = WindDirectionType.NorthEast;
                    break;
                case "nw":
                    wd = WindDirectionType.NorthWest;
                    break;

                default: wd = WindDirectionType.East;
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
                try
                {
                    result = query.First();
                }
                catch (Exception)
                {
                    result = null;
                }
                
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
                case 6:
                    result = DayTimeType.morning;
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

                default:
                    result = DayTimeType.morning;
                    break;
            }

            return result;
        }
    }
}
