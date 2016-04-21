/// MessageLocation class / Stanislav Kuzmich / Art.Stea1th

using Telegram.Bot.Types;

namespace WeatherBot.TeleInteraction.TelegramAdapters
{
    public class MLocation
    {
        private readonly Location _location;

        public MLocation(Location location)
        {
            _location = location;
        }

        /// <summary>
        ///     Latitude as defined by sender
        /// </summary>
        public float Latitude
        {
            get { return _location.Latitude; }
        }

        /// <summary>
        ///     Longitude as defined by sender
        /// </summary>
        public float Longitude
        {
            get { return _location.Longitude; }
        }
    }
}