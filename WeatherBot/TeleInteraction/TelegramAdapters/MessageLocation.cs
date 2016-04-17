namespace WeatherBot.TeleInteraction.TelegramAdapters {


    public class MessageLocation {

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

        public MessageLocation(Telegram.Bot.Types.Location location) {
            _location = location;
        }
    }
}