///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///


namespace WeatherBot.MessagesConveyor.TeleInteraction.Adapters {

    internal sealed class Location {

        public readonly float Latitude;
        public readonly float Longitude;

        public Location(Telegram.Bot.Types.Location location) {

            Latitude  = location != null ? location.Latitude  : 0;
            Longitude = location != null ? location.Longitude : 0;

        }
    }
}