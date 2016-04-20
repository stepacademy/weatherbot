using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Test.CheckUpdateWeather.Service_References.WSLweatherReference;


namespace Test.CheckUpdateWeather
{
    class Program
    {
        static void Main(string[] args)
        {   
            var proxy = new WeatherClient();

            proxy.UpdateWeather();
        }
    }
}
