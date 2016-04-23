///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Runtime.Serialization;

namespace WeatherBot.TeleInteraction.Adapters {

    [DataContract]
    public class MLocation {

        [DataMember] public readonly float Latitude;
        [DataMember] public readonly float Longitude;

        public MLocation(Telegram.Bot.Types.Location location) {

            Latitude  = location != null ? location.Latitude  : 0;
            Longitude = location != null ? location.Longitude : 0;

        }
    }
}