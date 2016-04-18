
/// Message class / Stanislav Kuzmich / Art.Stea1th

namespace WeatherBot.TeleInteraction.TelegramAdapters {

    using System;

    public class Message {

        private Telegram.Bot.Types.Message _message;

        private MessageUser _user;
        private MessageLocation _location;        
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

        /// <summary>
        /// Message sender
        /// </summary>
        public MessageUser User {
            get {
                return _user;
            }
        }

        /// <summary>
        /// Location
        /// </summary>
        public MessageLocation Location {
            get {
                return _location;
            }
        }
        
        /// <summary>
        /// new MessageResponse attach here
        /// </summary>
        public MessageResponse Response {
            get {
                return _response;
            }
            set {
                _response = value;
            }
        }

        public Message(Telegram.Bot.Types.Update update) {

            _message  = update.Message;
            _user     = new MessageUser(update.Message.From);
            _location = new MessageLocation(update.Message.Location);
            Response  = null;
        }

    }
}