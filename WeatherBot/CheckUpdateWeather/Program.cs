using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


namespace WeatherBot.CheckUpdateWeather
{
    class Program
    {
        static void Main(string[] args)
        {   
            var proxy = new WSLweatherReference.WeatherClient();

            proxy.UpdateWeather();
        }
    }
}
