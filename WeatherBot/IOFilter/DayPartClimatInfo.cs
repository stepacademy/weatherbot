using System.Text;

namespace WeatherBot.IOFilter
{
    public class DayPartClimatInfo
    {
        public enum DAY_PART_TYPE
        {
            MORINING,
            DAY,
            EVENING,
            NIGHT
        }

        public enum WEATHER_EVENTS
        {
            ONLY_SUN = 1,
            SUN_CLOUD = 2,
            CLOUD = 4,
            RAIN = 8,
            SUN_CLOUD_RAIN = 16,
            STORM = 32,
            STORM_RAIN_CLOUD = 64,
            SNOW = 128,
            HURRICANE = 256,
            FOG = 512,
            FIRE = 1024
        }

        public DAY_PART_TYPE day_part_type;
        public int weather_events;

        public DayPartClimatInfo(DAY_PART_TYPE type, double t, int p, int events)
        {
            day_part_type = type;
            weather_events = events;
            temperature = t;
            pressure = p;
        }

        public double temperature { get; private set; }
        public int pressure { get; private set; }

        public void SetTemperature(double t)
        {
            temperature = t;
        }

        public void SetPressure(int p)
        {
            pressure = p;
        }

        private string weather_events_string()
        {
            //"☀️🌤⛅️🌥🌦☁️🌧⛈🌩🌨🌪🌫🔥";
            var sb = new StringBuilder();
            if ((weather_events & (int) WEATHER_EVENTS.ONLY_SUN) == (int) WEATHER_EVENTS.ONLY_SUN)
                sb.Append("☀️");
            if ((weather_events & (int) WEATHER_EVENTS.CLOUD) == (int) WEATHER_EVENTS.CLOUD)
                sb.Append("☁️");
            if ((weather_events & (int) WEATHER_EVENTS.SUN_CLOUD) == (int) WEATHER_EVENTS.SUN_CLOUD)
                sb.Append("🌤");
            if ((weather_events & (int) WEATHER_EVENTS.RAIN) == (int) WEATHER_EVENTS.RAIN)
                sb.Append("🌦");
            if ((weather_events & (int) WEATHER_EVENTS.FOG) == (int) WEATHER_EVENTS.FOG)
                sb.Append("🌫");
            if ((weather_events & (int) WEATHER_EVENTS.FIRE) == (int) WEATHER_EVENTS.FIRE)
                sb.Append("🔥");
            if ((weather_events & (int) WEATHER_EVENTS.HURRICANE) == (int) WEATHER_EVENTS.HURRICANE)
                sb.Append("🌪");
            if ((weather_events & (int) WEATHER_EVENTS.SNOW) == (int) WEATHER_EVENTS.SNOW)
                sb.Append("🌨");
            return sb.ToString();
        }

        private string day_part_type_string()
        {
            switch (day_part_type)
            {
                case DAY_PART_TYPE.MORINING:
                    return "🌇" + "(утро)";
                case DAY_PART_TYPE.DAY:
                    return "🏙" + "(день)";
                case DAY_PART_TYPE.EVENING:
                    return "🌆" + "(веч. )";
                case DAY_PART_TYPE.NIGHT:
                    return "🌃" + "(ночь)";
            }
            return " ";
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("{0}🌡{1}{2}° {3}",
                day_part_type_string(),
                getsign(temperature), temperature,
                weather_events_string()
                ));
            return sb.ToString();
        }

        private string getsign(double num)
        {
            if (num <= 0) return "";
            return "+";
        }
    }
}