using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherBot.IOFilter
{
    public class ClimatInfo : IEnumerable<DayClimatInfo>, IEnumerator<DayClimatInfo>
    {
        public string city { get; private set; }
        public int subscrib { get; set; }

        private int _index;
        private Dictionary<DateTime, DayClimatInfo> _dates;
        
        public enum SUBSCRIPT // для подписки на определенное время
        {
            MORNING = 1,
            DAY = 2,
            EVENING = 4,
            NIGHT = 8
        }

        public ClimatInfo()
        {
            _index = -1; //IEnumerable
            _dates = new Dictionary<DateTime, DayClimatInfo>();
        }

        public void SetCity(string cityname)
        {
            city = cityname;
        }

        public void AddDateInfo(DayClimatInfo ci)
        {
            _dates[ci.date] = ci;
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

        #region  realisation of IEnumerable

        public int Count
        {
            get { return _dates.Count; }
        }

        public void Clear()
        {
            _dates.Clear();
        }

        public DayClimatInfo this[int index]
        {
            get { return _dates.ElementAt(index).Value; }
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
                    return _dates.ElementAt(_index).Value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get { return _dates.ElementAt(_index).Value; }
        }

        public void Dispose()
        {
            _dates.Clear();
        }

        public bool MoveNext()
        {
            ++_index;
            return _index < _dates.Count;
        }

        public void Reset()
        {
            _index = -1;
        }
        #endregion
    }
}