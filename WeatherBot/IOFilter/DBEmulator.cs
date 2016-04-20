using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherBot.IOFilter
{
    /// <summary>
    ///  Эмуляция запросов к базе  "Cities.txt"
    /// </summary>
    public class DBEmulator
    {
        public enum LAGUAGE {RUSSIAN, ENGLISH}
        public void LoadCities(List<string> cities)
        {
            using (StreamReader sr = new StreamReader(new FileStream("Cities.txt", FileMode.Open), Encoding.UTF8))
            {
                while (sr.EndOfStream != true)
                {
                    cities.Add(sr.ReadLine());
                }
            }
        }
        public void LoadDayPartsDictionary(Dictionary<string, int> day_parts)
        {
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
        }
        public void LoadDateInWordDictionary(Dictionary<string, int> dateInWord)
        {
            dateInWord.Add("сегодня", 0);
            dateInWord.Add("завтра", 1);
            dateInWord.Add("послезавтра", 2);
            dateInWord.Add("вчера", -1);
            dateInWord.Add("позавчера", -1);
        }
    }
}

/*
 Понедельник - Monday/Mon
 Вторник - Tuesday/Tue
 Среда - Wednesday/Wed
 Чеверг - Thursday/Thu
 Пятница - Friday/Fri
 Суббота - Saturday/Sat
 Воскресенье - Sunday/Sun
*/
