
/// MessageLocation class / Stanislav Kuzmich / Art.Stea1th

namespace WeatherBot.TeleInteraction.TelegramAdapters {

    public class MLocation {

        Telegram.Bot.Types.Location _location;

        /// <summary>
        /// Latitude as defined by sender
        /// </summary>
        public float Latitude {
            get {
                return _location.Latitude;
            }
        }

        /// <summary>
        /// Longitude as defined by sender
        /// </summary>
        public float Longitude {
            get {
                return _location.Longitude;
            }
        }

        public MLocation(Telegram.Bot.Types.Location location) {
            _location = location;
        }
    }
}