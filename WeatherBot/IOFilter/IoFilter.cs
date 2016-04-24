using System;
using System.Collections.Generic;
using System.Linq;
using WeatherBot.TeleInteraction;
using WeatherBot.TeleInteraction.Adapters;
namespace WeatherBot.IOFilter
{
    public class IoFilter// : IMessageProcessor
    {
        public delegate void DebugOut(string debug_text);
        public DebugOut DebugOutEvent;
        /// -----------------------------------------------
        private readonly List<string> _Cities = new List<string>();
        private List<string> _Tokens = new List<string>();
        private readonly Dictionary<string, int> _dateInWord = new Dictionary<string, int>(); //из базы или файла
        private readonly Dictionary<string, string> _dayOfWeek = new Dictionary<string, string>(); //из базы или файла
        private readonly Dictionary<string, int> _day_parts = new Dictionary<string, int>(); //из базы или файла

        public IoFilter(DebugOut debugOutMethod)
        {
            DebugOutEvent += debugOutMethod;
            Initialize();
        }

        public IoFilter()
        {
            Initialize();
        }

        private void Initialize()
        {
            var DB = new DBEmulator();
            DB.LoadCities(_Cities);
            DB.LoadDayPartsDictionary(_day_parts);
            DB.LoadDateInWordDictionary(_dateInWord);
            DB.LoadDayOfWeekDictionary(_dayOfWeek);   //!!! пока не работает
            //ReceiveActionListener.Instance.MessageProcessingEventHandlers += MessageProcessing;
            //ReceiveActionListener.Instance.Start();
        }

        #region response
        public Message MessageProcessing(Message message)
        {
            if (message != null)
            {
                IncomeMessage(message.Text);
                message.Response = new MResponse();

                var msgout = OutcomeMessage();
                var i = 0;
                int.TryParse(message.Text[0].ToString(), out i);
                if (DebugOutEvent != null)
                    DebugOutEvent("\n[IN]:\n" + message.Text + " [" + i + "]" + "\n[OUT]:\n" + msgout);
                message.Response.Text = msgout;

                return message;
            }
            return null;
        }

        public void IncomeMessage(string message)
        {
            _Tokens.Clear();
            _Tokens = BreakWords(message);
        }

        public string OutcomeMessage()
        {
            //    Тут происходит разбор
            //    нужно добавить обработку ключевых слов (неделя, и т.п.)
            //    Сделав разбор
            //    Отправит составленную структуру для заполнения данными
            var cli = new ClimatInfo();
            if (_Tokens.Count == 0) return "Введите что-нибудь..."; // запрос к базе
            foreach (var str in _Tokens)
            {
                var token = new string(str.ToArray());

                FindDateInWord(cli, str);

                var city = str;
                if (ContainsInLowerCase(_Cities, ref city))
                    cli.SetCity(city);

                var subscr = cli.subscrib;
                if (IsDayPart(str, ref subscr))
                    cli.subscrib = subscr;
            }
            if (cli.subscrib == 0)
                cli.subscrib = (int)ClimatInfo.SUBSCRIPT.MORNING + (int)ClimatInfo.SUBSCRIPT.DAY
                              + (int)ClimatInfo.SUBSCRIPT.EVENING + (int)ClimatInfo.SUBSCRIPT.NIGHT;

            if (cli.Count == 0)
                AddDateToClimatInfo(cli, DateTime.Now);

            var check = "";
            if (NotCorrectMessageAnswer(cli, ref check))
                return check;

            // Запрос к базе 
            for (var i = 0; i < cli.Count; ++i)
            {
                DayPartClimatInfo m=null, d=null, e=null, n=null;
                if ((cli.subscrib & (int)ClimatInfo.SUBSCRIPT.MORNING) == (int)ClimatInfo.SUBSCRIPT.MORNING)
                {
                    m = new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.MORINING, 5, 120,
                        (int)DayPartClimatInfo.WEATHER_EVENTS.FOG);
                }
                if ((cli.subscrib & (int)ClimatInfo.SUBSCRIPT.DAY) == (int)ClimatInfo.SUBSCRIPT.DAY)
                {
                    d = new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.DAY, 14, 120,
                        (int)DayPartClimatInfo.WEATHER_EVENTS.CLOUD);
                }
                if ((cli.subscrib & (int)ClimatInfo.SUBSCRIPT.EVENING) == (int)ClimatInfo.SUBSCRIPT.EVENING)
                {
                    e = new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.EVENING, 20, 120,
                        (int)DayPartClimatInfo.WEATHER_EVENTS.ONLY_SUN);
                }
                if ((cli.subscrib & (int)ClimatInfo.SUBSCRIPT.NIGHT) == (int)ClimatInfo.SUBSCRIPT.NIGHT)
                {
                    n = new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.NIGHT, 7, 120,
                        (int)DayPartClimatInfo.WEATHER_EVENTS.CLOUD);
                }
                cli[i].SetInfo(m, d, e, n);
            }
            return cli.ToString();
        }

        private bool NotCorrectMessageAnswer(ClimatInfo cli, ref string answer)
        {
            answer = "Вы"; // проработать грамматику ответа
            if (cli.city == null) answer += " не указали свой город "; // запросы к базе
            if (cli.Count == 0) answer += " не выбрали дни ";
            return answer != "Вы";
        }


        private bool ContainsInLowerCase(List<string> words, ref string search)
        {
            foreach (var test in words)
            {
                if (search.ToLower() == test.ToLower())
                {
                    search = test;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region make tokens

        private string PrepareMessage(string message)
        {
            // тире может использоваться в городах и диапазонах!!!
            // точки в датах!!! 
            // проверить корректность перевода строки
            const string breaksymols = "!@#$%^&*()+=:,;_'?<>{}\n";
            var ret = new string(message.ToCharArray());
            foreach (var ch in breaksymols)
                if (ret.Contains(ch))
                    ret = ret.Replace(ch, ' ');
            return ret;
        }

        private List<string> BreakWords(string message)
        {
            message = PrepareMessage(message).ToLower();
            var words = message.Split(' ');
            var ret = words.ToList<string>();
            return ret;
        }

        #endregion

        #region working with date

        private void FindDateInWord(ClimatInfo cli, string str)
        {
            var dt = new DateTime();
            int day;
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
        }

        private void AddDateToClimatInfo(ClimatInfo cli, DateTime dt)
        {
            var dcli = new DayClimatInfo();
            dcli.SetDate(dt);
            cli.AddDateInfo(dcli);
        }

        private bool DayOfWeekToDate(string day, ref DateTime dateret)
        {
            var days = new Dictionary<string, DateTime>(); //брать из базы?
            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            for (var i = 1; i < 7; ++i) //  учет сегодня(?!)
            {
                dt = dt + new TimeSpan(24, 0, 0);
                days[string.Format("{0:ddd}", dt).ToLower()] = dt;
                days[string.Format("{0:dddd}", dt).ToLower()] = dt;
            }

            var test = new string(day.ToLower().ToCharArray());
            foreach (var dayname in days.Keys)
            {
                if (dayname == test)
                {
                    days.TryGetValue(dayname, out dateret);
                    return true;
                }
            }
            return false;
        }

        private bool IntToDate(int day, ref DateTime dateret)
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

        private bool IsDayPart(string str, ref int subscr)
        {
            var test = new string(str.ToCharArray());
            foreach (var day_part in _day_parts.Keys)
            {
                if (day_part == test)
                {
                    int ret;
                    _day_parts.TryGetValue(day_part, out ret);
                    subscr |= ret;
                    return true;
                }
            }
            return false;
        }
        // позавчера? вчера?
        private bool WordToDate(string word, ref DateTime dateret)
        {
            // добавить "сейчас", "после обеда", "через три часа" и т.д.
            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var test = word.ToLower();
            foreach (var baseword in _dateInWord.Keys)
            {
                if (test == baseword)
                {
                    var span = 0;
                    _dateInWord.TryGetValue(baseword, out span);
                    dt += new TimeSpan(24 * span, 0, 0);
                    dateret = dt;
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}