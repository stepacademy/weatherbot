using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using WeatherBot.TeleInteraction;

namespace WeatherBot.IOFilter
{
    ///
    ///  IOFilterator
    ///
    public class IOFilterator
    {
        //
        //ITeleInteractor test = new TeleInteractor();
        //
        List<string> tokens = new List<string>();
        List<string> Cities = new List<string>();

        public IOFilterator()
        {
            Cities = new DBEmulator().GetCountries();
        }

        public void IncomeMessage(string message)
        {
            tokens.Clear();
            if (message.Length == 0)
                throw (new ErrorMessage("Пустая строка сообщения!"));

            tokens = BreakWords(message);
        }

        private string PrepareMessage(string message)
        {
            // предусмотреть пользовательские символы диапазона и т.п.
            // тире может использоваться в городах!!!
            // точки в датах!!!
            // учитываем перевод строки
            const string breaksymols = "!@#$%^&*()+=:,;_'?<>{}\n";
            string ret = new string(message.ToCharArray());
            foreach (char ch in breaksymols.ToCharArray())
                if (ret.Contains(ch))
                    ret = ret.Replace(ch, ' ');
            return ret;
        }
        private List<string> BreakWords(string message)
        {
            message = PrepareMessage(message);
            string[] words = message.Split(' ');
            List<string> ret = new List<string>();

            foreach (string word in words)
                ret.Add(word);
            return ret;
        }

        public bool DayOfWeekToDate(string day, ref DateTime dateret)
        {
            Dictionary<string, DateTime> days = new Dictionary<string, DateTime>(); //брать из базы?
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            for (int i = 1; i < 7; ++i) //  учет сегодня(?!)
            {
                dt = dt + new TimeSpan(24, 0, 0);
                days[string.Format("{0:ddd}", dt).ToLower()] = dt;
                days[string.Format("{0:dddd}", dt).ToLower()] = dt;
            }

            string test = new string((day.ToLower()).ToCharArray());
            foreach (string dayname in days.Keys)
            {
                if (dayname == test)
                {
                    days.TryGetValue(dayname, out dateret);
                    return true;
                }
            }
            return false;
        }

        public bool IntToDate(int day, ref DateTime dateret)
        {
            DateTime dt;
            try
            {
                if (DateTime.Now.Day <= day) // еще этот месяц
                    dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);
                else
                {
                    if (DateTime.Now.Month < 12) //следущий месяц
                        dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, day);
                    else
                        dt = new DateTime(DateTime.Now.Year + 1, 1, day); //после НГ
                }
                dateret = dt;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool WordToDate(string word, ref DateTime dateret)
        {
            Dictionary<string, int> words = new Dictionary<string, int>(); //из базы или файла
            words.Add("сегодня", 0);
            words.Add("завтра", 1);
            words.Add("послезавтра", 2);
            words.Add("вчера", -1);
            words.Add("позавчера", -1);

            // часы и минуты не учитываются
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            string test = word.ToLower();

            foreach (string baseword in words.Keys)
            {
                if (test == baseword)
                {
                    int span = 0;
                    words.TryGetValue(baseword, out span);
                    dt += new TimeSpan(24 * span, 0, 0);
                    dateret = dt;
                    return true;
                }
            }
            return false;
        }

        private void AddDateToClimatInfo(ClimatInfo cli, DateTime dt)
        {
            DayClimatInfo dcli = new DayClimatInfo();
            dcli.SetDate(dt);
            cli.AddDateInfo(dcli);
        }

        /// <summary>
        ///    Тут будет происходить разбор
        ///    нужно добавить обработку ключевых слов (неделя,утро и т.п.)
        ///    Сделав разбор
        ///    Отправит составленную структуру для заполнения данными
        ///    После заполнения функция сформирует текст (картинку) для пользователя
        ///    Естественно все будет разбиваться на классы и методы
        /// </summary>
        /// <returns></returns>
        public string OutcomeMessage()
        {
            ClimatInfo cli = new ClimatInfo();
            int day = 0;
            foreach (string str in tokens)
            {
                string token = new string(str.ToArray());
                DateTime dt = new DateTime();

                if (WordToDate(str, ref dt))
                {
                    AddDateToClimatInfo(cli, dt);
                }
                else if (DateTime.TryParse(str, out dt))
                {
                    AddDateToClimatInfo(cli, dt);
                }
                else if (int.TryParse(str, out day))
                {
                    if (IntToDate(day, ref dt))
                    {
                        AddDateToClimatInfo(cli, dt);
                    }
                }
                else if (DayOfWeekToDate(str, ref dt))
                {
                    AddDateToClimatInfo(cli, dt);
                }

                if (Cities.Contains(str))
                {
                    cli.SetCity(str);
                }
            }
            return cli.ToString();
        }
    }

    /// <summary>
    ///  Эмуляция запросов к базе
    /// </summary>
    public class DBEmulator
    {
        public List<string> GetCountries()
        {
            List<string> ret = new List<string>();
            using (StreamReader sr = new StreamReader(new FileStream("..\\..\\Resources\\Cities.txt", FileMode.Open), Encoding.UTF8))
            {
                while (sr.EndOfStream != true)
                {
                    ret.Add(sr.ReadLine());
                }
            }
            return ret;
        }
    }

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
        public void SetInfo(DayPartClimatInfo m,
                           DayPartClimatInfo d,
                           DayPartClimatInfo e,
                           DayPartClimatInfo n)
        {
            morning = m; day = d; evening = e; night = n;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("{0:ddd, d MMM yyyy}", date));
            if (morning != null) sb.AppendLine("  утром:" + morning.ToString());
            if (day != null) sb.AppendLine("   днем:" + day.ToString());
            if (evening != null) sb.AppendLine("вечером:" + evening.ToString());
            if (night != null) sb.AppendLine("  ночью:" + night.ToString());
            return sb.ToString();
        }
    }

    public class ClimatInfo : IEnumerable<DayClimatInfo>, IEnumerator<DayClimatInfo>
    {
        public string city { get; private set; }
        private Dictionary<DateTime, DayClimatInfo> dates { get; set; }

        // для подписки на определенное время вероятно будет храниться в настройках пользователя
        public enum SUBSCRIPT { MORNING = 1, DAY = 2, EVENING = 4, NIGHT = 8 };

        private int index;

        public ClimatInfo()
        {
            index = -1;
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
            }
            return sb.ToString();
        }

        public DayClimatInfo Current
        {
            get
            {
                try
                {
                    return dates.ElementAt(index).Value;
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
                return (Object)dates.ElementAt(index).Value; //throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            dates.Clear();
        }

        public bool MoveNext()
        {
            ++index;
            return (index < dates.Count);
        }

        public void Reset()
        {
            index = -1;
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

    public class ErrorMessage : SystemException
    {
        public string error { get; private set; }

        public ErrorMessage() : base()
        {

        }
        public ErrorMessage(string message) : base(message)
        {
            error = message;
        }
        public ErrorMessage(string message, Exception innerException) : base(message, innerException)
        {

        }
        //protected ErrorMessage(SerializationInfo info, StreamingContext context)
    }
}
