using System;
using System.Text;
using System.Threading;
using System.Xml;

namespace Test.CheckCities
{
    internal class Program
    {
        private static Semaphore _pool;


        private static readonly StringBuilder _msg = new StringBuilder();

        private static void Main(string[] args)
        {
            UpdateCities();
        }


        private static void Worker(object countryElement)
        {
            var country = (XmlElement) countryElement;

            // Console.WriteLine("{0} ждет", country.GetAttribute("name"));

            _pool.WaitOne();

            //Console.WriteLine("Старт {0}", country.GetAttribute("name"));

            foreach (XmlNode cityNode in country)
            {
                if (cityNode.Attributes == null) continue;

                var weatherInfo = new XmlDocument();
                try
                {
                    weatherInfo.Load(
                        $@"http://export.yandex.ru/weather-ng/forecasts/{cityNode.Attributes.GetNamedItem("id")
                            .InnerText}.xml");
                }
                catch (Exception ex)
                {
                    _msg.AppendFormat("no load id: {3, -5} city: {0,-25} country: {1,-15} {2,-20}", cityNode.InnerText,
                        country.GetAttribute("name"), ex.Message, cityNode.Attributes.GetNamedItem("id").InnerText);
                    _msg.AppendLine();
                }
            }

            _pool.Release();

            Progress.Curr++;
            Progress.DrawTextProgressBar(_msg.ToString());


            //Console.WriteLine("Готово {0}", country.GetAttribute("name"));
        }

        private static void UpdateCities()
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

            Progress.TotalProgress = doc.DocumentElement.ChildNodes.Count - 1;

            foreach (XmlElement countryElement in doc.DocumentElement)
            {
                var t = new Thread(Worker);
                t.Start(countryElement);
            }
        }
    }
}