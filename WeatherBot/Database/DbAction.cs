using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBot.Database.Entities;

namespace WeatherBot.Database
{
    public static class DbAction
    {
        public static WindDirectionType GetWindDirectionType(string windDirection)
        {
            WindDirectionType wd;

            switch (windDirection)
            {
                case "s":
                    wd = WindDirectionType.South;
                    break;
                case "w":
                    wd = WindDirectionType.West;
                    break;
                case "e":
                    wd = WindDirectionType.East;
                    break;
                case "n":
                    wd = WindDirectionType.North;
                    break;
                case "se":
                    wd = WindDirectionType.SouthEast;
                    break;
                case "sw":
                    wd = WindDirectionType.SouthWest;
                    break;
                case "ne":
                    wd = WindDirectionType.NorthEast;
                    break;
                case "nw":
                    wd = WindDirectionType.NorthWest;
                    break;

                default: wd = WindDirectionType.East;
                    break;
            }

            return wd;
        }

        public static WeatherState GetWeatherState(string stateCode)
        {
            WeatherState result;
            using (var db = new WeatherDbContext())
            {
                db.WeatherStates.Load();
                var query = from wst in db.WeatherStates where wst.Code == stateCode select wst;
                try
                {
                    result = query.First();
                }
                catch (Exception)
                {
                    result = null;
                }
                
            }

            return result;
        }

        public static DayTimeType GetDayTimeType(string dayTime)
        {
            DayTimeType result;
            Enum.TryParse(dayTime, out result);

            return result;
        }

        public static DayTimeType GetDayTimeType(int hour)
        {
            DayTimeType result;

            switch (hour)
            {
                case 6:
                    result = DayTimeType.morning;
                    break;
                case 12:
                    result = DayTimeType.day;
                    break;
                case 18:
                    result = DayTimeType.evening;
                    break;
                case 0:
                    result = DayTimeType.night;
                    break;

                default:
                    result = DayTimeType.morning;
                    break;
            }

            return result;
        }
    }
}
