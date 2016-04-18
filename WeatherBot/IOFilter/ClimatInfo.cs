using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WeatherBot.IOTranslator
{
    public class ClimatInfo : IEnumerable<DayClimatInfo>, IEnumerator<DayClimatInfo>
    {
        public string city { get; private set; }
        private Dictionary<DateTime, DayClimatInfo> dates { get; set; }

        // для подписки на определенное время вероятно будет храниться в настройках пользователя
        public enum SUBSCRIPT { MORNING = 1, DAY = 2, EVENING = 4, NIGHT = 8 };
        public int subsrib { get; set; }
        private int _index;

        public ClimatInfo()
        {
            _index = -1;
            dates = new Dictionary<DateTime, DayClimatInfo>();
        }

        public void SetCity(string cityname)
        {
            city = cityname;
        }

        public int Count
        {
            get { return dates.Count; }
        }

        public void AddDateInfo(DayClimatInfo ci)
        {
            dates[ci.date] = ci;
        }

        public DayClimatInfo this[int index]
        {
            get
            {
                return dates.ElementAt(index).Value;
            }
        }

        public void Clear()
        {
            dates.Clear();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("В городе:" + city);
            foreach (DayClimatInfo dci in this)
            {
                sb.AppendLine(dci.ToString());
                /// нужно перенести в другое место (только для демонстрации)
                if ((subsrib & (int)SUBSCRIPT.MORNING) == (int)SUBSCRIPT.MORNING) sb.AppendLine("\t    утром: +7t давл 260");
                if ((subsrib & (int)SUBSCRIPT.DAY) == (int)SUBSCRIPT.DAY) sb.AppendLine("\t     днем: +12t");
                if ((subsrib & (int)SUBSCRIPT.EVENING) == (int)SUBSCRIPT.EVENING) sb.AppendLine("\tвечером: +13t" + 7);
                if ((subsrib & (int)SUBSCRIPT.NIGHT) == (int)SUBSCRIPT.NIGHT) sb.AppendLine("\t    ночью: +0t" + 8);
            }
            return sb.ToString();
        }

        public DayClimatInfo Current
        {
            get
            {
                try
                {
                    return dates.ElementAt(_index).Value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (Object)dates.ElementAt(_index).Value;
            }
        }

        public void Dispose()
        {
            dates.Clear();
        }

        public bool MoveNext()
        {
            ++_index;
            return (_index < dates.Count);
        }

        public void Reset()
        {
            _index = -1;
        }

        public IEnumerator<DayClimatInfo> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
