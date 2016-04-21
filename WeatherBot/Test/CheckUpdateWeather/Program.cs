using Test.CheckUpdateWeather.Service_References.WSLweatherReference;

namespace Test.CheckUpdateWeather
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var proxy = new WeatherClient();

            proxy.UpdateWeather();
        }
    }
}