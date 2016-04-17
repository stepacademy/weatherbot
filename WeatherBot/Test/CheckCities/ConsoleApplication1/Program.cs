using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Test.CheckCities
{
    class Program
    {
        private static Semaphore _pool;

        static void Main(string[] args)
        {

            UpdateCities();

        }

        static void Worker(Object countryElement)
        {
            var country = (XmlElement)countryElement;

            Console.WriteLine("Поток {0} ждет", country.GetAttribute("name"));

            _pool.WaitOne();

            Console.WriteLine("Старт {0}", country.GetAttribute("name"));

            foreach (XmlNode cityNode in country)
            {
                if (cityNode.Attributes != null)
                {
                    //Console.WriteLine("\t"+cityNode.Attributes.GetNamedItem("id").InnerText + " " + cityNode.InnerText);
                    var weatherInfo = new XmlDocument();
                    try
                    {
                        weatherInfo.Load(
                            $@"http://export.yandex.ru/weather-ng/forecasts/{cityNode.Attributes.GetNamedItem("id")
                                .InnerText}.xml");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("----no load city: {0}, country: {1}----", cityNode.InnerText, country.GetAttribute("name"));
                        Console.WriteLine("\t{0}", ex.Message);
                    }
                }
                
            }

            _pool.Release();
            Console.WriteLine("Готово {0}", country.GetAttribute("name"));
        }

        static void UpdateCities()
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
            
            //свободных 2
            _pool = new Semaphore(2, 10);

            foreach (XmlElement countryElement in doc.DocumentElement)
            {
                //Console.WriteLine(countryElement.GetAttribute("name"));

                var t = new Thread(new ParameterizedThreadStart(Worker));
                t.Start(countryElement);
                
            }
            
        }
    }
}
