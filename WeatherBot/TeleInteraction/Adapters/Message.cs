///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.Runtime.Serialization;

namespace WeatherBot.TeleInteraction.Adapters {
    
    [DataContract]
    public class Message {

        private Telegram.Bot.Types.Message _message;

        private MUser _user;
        private MLocation _location;        
        private MResponse _response;

        [DataMember]
        public int Id {
            get {
                return _message.MessageId;
            }
        }

        [DataMember]
        public DateTime DTime {
            get {
                return _message.Date;
            }
        }

        [DataMember]
        public string Text {
            get {
                return _message.Text;
            }
        }

        [DataMember]
        public MUser User {
            get {
                return _user;
            }
        }

        [DataMember]
        public MLocation Location {
            get {
                return _location;
            }
        }
        
        [DataMember]
        public MResponse Response {
            get {
                return _response;
            }
            set {
                _response = value;
            }
        }

        public Message(Telegram.Bot.Types.Update update) {

            _message  = update.Message;
            _user     = new MUser(update.Message.From);
            _location = new MLocation(update.Message.Location);
            Response  = null;
        }
    }
}