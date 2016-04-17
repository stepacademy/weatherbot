using System;
using System.Collections.Generic;
using System.Linq;
using WeatherBot.TeleInteraction;
//using Weatherbot.WSLweather;
//using WeatherBot.TeleInteraction;
using WeatherBot.TeleInteraction.TelegramAdapters;

namespace WeatherBot.IOFilter
{
    ///
    ///  IOFilterator
    ///
    public class IOFilterator
    {
        public delegate void  DebugOut(string debug_text);
        private List<string> _Tokens = new List<string>();
        private List<string> _Cities = new List<string>();
        public DebugOut DebugOutEvent;
  
        private Message MessageProcessing(Message message)
        {

            if (message != null)
            {
                IncomeMessage(message.Text);
                message.Response = new MessageResponse();

                string msgout = OutcomeMessage();
                if (DebugOutEvent != null)
                    DebugOutEvent("[IN]:\n" + message.Text +"\n[OUT]:\n"+ msgout);
                message.Response.Text = msgout;

                return message;
            }
            return null;
        }

        public IOFilterator()
        {
            _Cities = new DBEmulator().GetCountries();
            InteractionProcess.Instance.ProcessingEventHandlers += MessageProcessing;
            InteractionProcess.Instance.State = InteractionProcessState.Launched;

           
        }

        public void IncomeMessage(string message)
        {
            _Tokens.Clear();
            _Tokens = BreakWords(message);
        }

        /// <summary>
        ///    Тут происходит разбор
        ///    нужно добавить обработку ключевых слов (неделя, и т.п.)
        ///    Сделав разбор
        ///    Отправит составленную структуру для заполнения данными
        ///    После заполнения функция сформирует текст (картинку) для пользователя
        ///    Естественно все будет разбиваться на классы и методы
        /// </summary>
        /// <returns></returns>
        public string OutcomeMessage()
        {
            ClimatInfo cli = new ClimatInfo();
            if (_Tokens.Count == 0) return "Введите что-нибудь...";  // запрос к базе
            foreach (string str in _Tokens)
            {
                string token = new string(str.ToArray());

                FindDateInWord(cli, str);

                if (_Cities.Contains(str))
                    cli.SetCity(str);

                int subscr = cli.subsrib;
                if (IsDayPart(str, ref subscr))
                    cli.subsrib = subscr;

            }
            if (cli.subsrib == 0)
                cli.subsrib = (int)ClimatInfo.SUBSCRIPT.MORNING + (int)ClimatInfo.SUBSCRIPT.DAY
                            + (int)ClimatInfo.SUBSCRIPT.EVENING + (int)ClimatInfo.SUBSCRIPT.NIGHT;

            string check = "";
            if (NotCorrectMessageAnswer(cli, ref check))
                return check;

            return cli.ToString();
        }

        private string PrepareMessage(string message)
        {
            // предусмотреть пользовательские символы диапазона и т.п.
            // тире может использоваться в городах!!!
            // точки в датах!!! учитываем перевод строки
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

        private bool DayOfWeekToDate(string day, ref DateTime dateret)
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
            Dictionary<string, int> day_parts = new Dictionary<string, int>();
            day_parts.Add("утро", (int)ClimatInfo.SUBSCRIPT.MORNING);
            day_parts.Add("день", (int)ClimatInfo.SUBSCRIPT.DAY);
            day_parts.Add("вечер", (int)ClimatInfo.SUBSCRIPT.EVENING);
            day_parts.Add("ночь", (int)ClimatInfo.SUBSCRIPT.NIGHT);
            day_parts.Add("утром", (int)ClimatInfo.SUBSCRIPT.MORNING);
            day_parts.Add("днем", (int)ClimatInfo.SUBSCRIPT.DAY);
            day_parts.Add("вечером", (int)ClimatInfo.SUBSCRIPT.EVENING);
            day_parts.Add("ночью", (int)ClimatInfo.SUBSCRIPT.NIGHT);
            day_parts.Add("утру", (int)ClimatInfo.SUBSCRIPT.MORNING);
            day_parts.Add("дню", (int)ClimatInfo.SUBSCRIPT.DAY);
            day_parts.Add("вечеру", (int)ClimatInfo.SUBSCRIPT.EVENING);
            day_parts.Add("ночи", (int)ClimatInfo.SUBSCRIPT.NIGHT);

            string test = new string(str.ToCharArray());
            foreach (string day_part in day_parts.Keys)
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

        private void FindDateInWord(ClimatInfo cli, string str)
        {
            DateTime dt = new DateTime();
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

        private bool NotCorrectMessageAnswer(ClimatInfo cli, ref string answer)
        {
            answer = "Вы";
            if (cli.city == null) answer += ", не указали свой город";   // запросы к базе
            if (cli.Count == 0) answer += ", не выбрали дни";
            return  answer != "Вы";
        }
    }



}
