using System.Text;

namespace WeatherBot.IOTranslator
{
    public class DayPartClimatInfo
    {
        public double temperature { get; private set; }
        public int pressure { get; private set; }

        public DayPartClimatInfo(double t, int p)
        {
            temperature = t;
            pressure = p;
        }

        public void SetTemperature(double t)
        {
            temperature = t;
        }

        public void SetPressure(int p)
        {
            pressure = p;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("температура" + string.Format("{0:.00}", temperature));
            sb.Append(",давление" + string.Format("{0:}", pressure));
            return sb.ToString();
        }
    }
}
