using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherBot.IOFilter
{
    /// <summary>
    ///  Эмуляция запросов к базе  "..\\..\\Resources\\Cities.txt"
    /// </summary>
    public class DBEmulator
    {
        public List<string> GetCountries()
        {
            List<string> ret = new List<string>();
            using (StreamReader sr = new StreamReader(new FileStream("Cities.txt", FileMode.Open), Encoding.UTF8))
            {
                while (sr.EndOfStream != true)
                {
                    ret.Add(sr.ReadLine());
                }
            }
            return ret;
        }
    }

}
