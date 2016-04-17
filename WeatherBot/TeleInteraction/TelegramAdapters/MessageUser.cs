namespace WeatherBot.TeleInteraction.TelegramAdapters {


    public class MessageUser {

        Telegram.Bot.Types.User _user;

        /// <summary>
        /// Unique identifier for this user
        /// </summary>
        public int Id {
            get {
                return _user.Id;
            }
        }

        /// <summary>
        /// User‘s username
        /// </summary>
        public string Username {
            get {
                return _user.Username;
            }
        }

        /// <summary>
        /// User‘s first name
        /// </summary>
        public string FirstName {
            get {
                return _user.FirstName;
            }
        }

        /// <summary>
        /// User‘s last name
        /// </summary>
        public string LastName {
            get {
                return _user.Username;
            }
        }

        public MessageUser(Telegram.Bot.Types.User user) {
            _user = user;
        }
    }
}