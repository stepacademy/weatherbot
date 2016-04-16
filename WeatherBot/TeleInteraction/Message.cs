namespace WeatherBot.TeleInteraction {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Message {

        private Telegram.Bot.Types.Message _message;

        /// <summary>
        /// Unique message identifier
        /// </summary>
        public int Id {
            get {
                return _message.MessageId;
            }
        }

        /// <summary>
        /// Date the message was sent in Unix time
        /// </summary>
        public DateTime DTime {
            get {
                return _message.Date;
            }
        }

        ///<summary>
        /// The actual UTF-8 text of the message
        /// </summary>
        public string Text {
            get {
                return _message.Text;
            }
        }

        public Message(Telegram.Bot.Types.Update update) {
            _message = update.Message;
        }
    }
}