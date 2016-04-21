using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherBot.IOFilter
{
    public class ClimatInfo : IEnumerable<DayClimatInfo>, IEnumerator<DayClimatInfo>
    {
        // для подписки на определенное время вероятно будет храниться в настройках пользователя
        public enum SUBSCRIPT
        {
            MORNING = 1,
            DAY = 2,
            EVENING = 4,
            NIGHT = 8
        }

        private int _index;

        public ClimatInfo()
        {
            _index = -1;
            dates = new Dictionary<DateTime, DayClimatInfo>();
        }

        public string city { get; private set; }
        private Dictionary<DateTime, DayClimatInfo> dates { get; }
        public int subsrib { get; set; }

        public int Count
        {
            get { return dates.Count; }
        }

        public DayClimatInfo this[int index]
        {
            get { return dates.ElementAt(index).Value; }
        }

        public IEnumerator<DayClimatInfo> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            get { return dates.ElementAt(_index).Value; }
        }

        public void Dispose()
        {
            dates.Clear();
        }

        public bool MoveNext()
        {
            ++_index;
            return _index < dates.Count;
        }

        public void Reset()
        {
            _index = -1;
        }

        public void SetCity(string cityname)
        {
            city = cityname;
        }

        public void AddDateInfo(DayClimatInfo ci)
        {
            dates[ci.date] = ci;
        }

        public void Clear()
        {
            dates.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("🏘" + city);
            foreach (var dci in this)
            {
                sb.AppendLine(dci.ToString());
            }
            return sb.ToString();
        }
    }
}