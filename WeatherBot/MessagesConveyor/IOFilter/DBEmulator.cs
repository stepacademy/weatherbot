﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherBot.MessagesConveyor.IOFilter
{
    /// <summary>
    ///     Эмуляция запросов к базе  "Cities.txt"
    /// </summary>
    public class DBEmulator
    {
        public enum LAGUAGE
        {
            RUSSIAN,
            ENGLISH
        }

        public void LoadCities(List<string> cities)
        {
            using (var sr = new StreamReader(new FileStream("Cities.txt", FileMode.Open), Encoding.UTF8))
            {
                while (sr.EndOfStream != true)
                {
                    cities.Add(sr.ReadLine());
                }
            }
        }

        public void LoadDayPartsDictionary(Dictionary<string, int> day_parts)
        {
            day_parts.Add("утро", (int) ClimatInfo.SUBSCRIPT.MORNING);
            day_parts.Add("день", (int) ClimatInfo.SUBSCRIPT.DAY);
            day_parts.Add("вечер", (int) ClimatInfo.SUBSCRIPT.EVENING);
            day_parts.Add("ночь", (int) ClimatInfo.SUBSCRIPT.NIGHT);
            day_parts.Add("утром", (int) ClimatInfo.SUBSCRIPT.MORNING);
            day_parts.Add("днем", (int) ClimatInfo.SUBSCRIPT.DAY);
            day_parts.Add("вечером", (int) ClimatInfo.SUBSCRIPT.EVENING);
            day_parts.Add("ночью", (int) ClimatInfo.SUBSCRIPT.NIGHT);
            day_parts.Add("утру", (int) ClimatInfo.SUBSCRIPT.MORNING);
            day_parts.Add("дню", (int) ClimatInfo.SUBSCRIPT.DAY);
            day_parts.Add("вечеру", (int) ClimatInfo.SUBSCRIPT.EVENING);
            day_parts.Add("ночи", (int) ClimatInfo.SUBSCRIPT.NIGHT);
        }

        internal void LoadDayOfWeekDictionary(Dictionary<string, string> dayOfWeek)
        {
            dayOfWeek.Add("пн", "пн");
            dayOfWeek.Add("вт", "вт");
            dayOfWeek.Add("ср", "ср");
            dayOfWeek.Add("чт", "чт");
            dayOfWeek.Add("пт", "пт");
            dayOfWeek.Add("сб", "сб");
            dayOfWeek.Add("вс", "вс");

            dayOfWeek.Add("понедельник", "пн");
            dayOfWeek.Add("вторник", "вт");
            dayOfWeek.Add("среда", "ср");
            dayOfWeek.Add("чеверг", "чт");
            dayOfWeek.Add("пятница", "пт");
            dayOfWeek.Add("суббота", "сб");
            dayOfWeek.Add("воскресенье", "вс");

            dayOfWeek.Add("mo", "пн");
            dayOfWeek.Add("tu", "вт");
            dayOfWeek.Add("we", "ср");
            dayOfWeek.Add("th", "чт");
            dayOfWeek.Add("fr", "пт");
            dayOfWeek.Add("sa", "сб");
            dayOfWeek.Add("su", "вс");

            dayOfWeek.Add("mon", "пн");
            dayOfWeek.Add("tue", "вт");
            dayOfWeek.Add("wed", "ср");
            dayOfWeek.Add("thu", "чт");
            dayOfWeek.Add("fri", "пт");
            dayOfWeek.Add("sat", "сб");
            dayOfWeek.Add("sun", "вс");

            dayOfWeek.Add("monday", "пн");
            dayOfWeek.Add("tuesday", "вт");
            dayOfWeek.Add("wednesday", "ср");
            dayOfWeek.Add("thursday", "чт");
            dayOfWeek.Add("friday", "пт");
            dayOfWeek.Add("saturday", "сб");
            dayOfWeek.Add("sunday", "вс");
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