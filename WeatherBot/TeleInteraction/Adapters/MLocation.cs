///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Runtime.Serialization;

namespace WeatherBot.TeleInteraction.Adapters {

    [DataContract]
    public class MLocation {

        Telegram.Bot.Types.Location _location;

        [DataMember]
        public float Latitude {
            get {
                return _location.Latitude;
            }
        }

        [DataMember]
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