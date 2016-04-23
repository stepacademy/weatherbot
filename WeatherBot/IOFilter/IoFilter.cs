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

        private readonly List<string> _Cities = new List<string>();
        private List<string> _Tokens = new List<string>();
        private readonly Dictionary<string, int> dateInWord = new Dictionary<string, int>(); //из базы или файла
        private readonly Dictionary<string, int> day_parts = new Dictionary<string, int>(); //из базы или файла
        public DebugOut DebugOutEvent;

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
            DB.LoadDayPartsDictionary(day_parts);
            DB.LoadDateInWordDictionary(dateInWord);
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

                var subscr = cli.subsrib;
                if (IsDayPart(str, ref subscr))
                    cli.subsrib = subscr;
            }
            if (cli.subsrib == 0)
                cli.subsrib = (int) ClimatInfo.SUBSCRIPT.MORNING + (int) ClimatInfo.SUBSCRIPT.DAY
                              + (int) ClimatInfo.SUBSCRIPT.EVENING + (int) ClimatInfo.SUBSCRIPT.NIGHT;

            if (cli.Count == 0)
                AddDateToClimatInfo(cli, DateTime.Now);

            var check = "";
            if (NotCorrectMessageAnswer(cli, ref check))
                return check;
            // Запрос к базе 
            for (var i = 0; i < cli.Count; ++i)
            {
                cli[i].SetInfo(new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.MORINING, 5, 120,
                    (int) DayPartClimatInfo.WEATHER_EVENTS.FOG),
                    new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.DAY, 20, 120,
                        (int) DayPartClimatInfo.WEATHER_EVENTS.ONLY_SUN),
                    new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.EVENING, 14, 120,
                        (int) DayPartClimatInfo.WEATHER_EVENTS.CLOUD),
                    new DayPartClimatInfo(DayPartClimatInfo.DAY_PART_TYPE.NIGHT, 0, 120,
                        (int) DayPartClimatInfo.WEATHER_EVENTS.CLOUD)
                    );
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

        private void AddDateToClimatInfo(ClimatInfo cli, DateTime dt)
        {
            var dcli = new DayClimatInfo();
            dcli.SetDate(dt);
            cli.AddDateInfo(dcli);
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
            // предусмотреть пользовательские символы диапазона и т.п.
            // тире может использоваться в городах!!!
            // точки в датах!!! учитываем перевод строки
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
            var ret = new List<string>();

            foreach (var word in words)
                ret.Add(word);
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
            foreach (var day_part in day_parts.Keys)
            {
                if (day_part == test)
                {
                    int ret;
                    day_parts.TryGetValue(day_part, out ret);
                    subscr |= ret;
                    return true;
                }
            }
            return false;
        }

        private bool WordToDate(string word, ref DateTime dateret)
        {
            // часы и минуты не учитываются
            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var test = word.ToLower();
            foreach (var baseword in dateInWord.Keys)
            {
                if (test == baseword)
                {
                    var span = 0;
                    dateInWord.TryGetValue(baseword, out span);
                    dt += new TimeSpan(24*span, 0, 0);
                    dateret = dt;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}