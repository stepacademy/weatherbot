///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Runtime.Serialization;

namespace WeatherBot.TeleInteraction.Adapters {

    [DataContract]
    public class MUser {

        [DataMember] public readonly int    Id;
        [DataMember] public readonly string Username;
        [DataMember] public readonly string FirstName;
        [DataMember] public readonly string LastName;

        public MUser(Telegram.Bot.Types.User user) {

            Id        = user.Id;
            Username  = user.Username;
            FirstName = user.FirstName;
            LastName  = user.LastName;

        }
    }
}