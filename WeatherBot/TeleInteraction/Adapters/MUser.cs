///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Runtime.Serialization;

namespace WeatherBot.TeleInteraction.Adapters {

    [DataContract]
    public class MUser {

        Telegram.Bot.Types.User _user;

        [DataMember]
        public int Id {
            get {
                return _user.Id;
            }
        }

        [DataMember]
        public string Username {
            get {
                return _user.Username;
            }
        }

        [DataMember]
        public string FirstName {
            get {
                return _user.FirstName;
            }
        }

        [DataMember]
        public string LastName {
            get {
                return _user.Username;
            }
        }

        public MUser(Telegram.Bot.Types.User user) {
            _user = user;
        }
    }
}