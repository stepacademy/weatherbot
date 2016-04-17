using System;


namespace WeatherBot.TeleInteraction.ReqResp {

    public class Message {

        private Telegram.Bot.Types.Message _message;
        private MessageResponse _response;

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

        public MessageResponse Response {
            get {
                return _response;
            }
            set {
                _response = value;
            }
        }

        public Message(Telegram.Bot.Types.Update update, MessageResponse response = null) {
            _message = update.Message;
        }

    }
}