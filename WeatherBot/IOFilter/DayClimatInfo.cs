using System;
using System.Text;

namespace WeatherBot.IOFilter
{
    public class DayClimatInfo
    {
        public DayPartClimatInfo morning { get; private set; }
        public DayPartClimatInfo day { get; private set; }
        public DayPartClimatInfo evening { get; private set; }
        public DayPartClimatInfo night { get; private set; }
        public DateTime date { get; private set; }

        public void SetDate(DateTime dt)
        {
            date = dt;
        }

        public void SetInfo(DayPartClimatInfo m, DayPartClimatInfo d,
                           DayPartClimatInfo e, DayPartClimatInfo n)
        {
            morning = m; day = d; evening = e; night = n;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("{0:ddd, d MMM yyyy}", date));
            //if (morning != null) sb.AppendLine("  утром:" + morning.ToString());
            //if (day != null) sb.AppendLine("   днем:" + day.ToString());
            //if (evening != null) sb.AppendLine("вечером:" + evening.ToString());
            //if (night != null) sb.AppendLine("  ночью:" + night.ToString());
            return sb.ToString();
        }
    }
}
