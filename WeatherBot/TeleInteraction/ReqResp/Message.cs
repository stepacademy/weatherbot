﻿using System;


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
        /// Unique chat-user identifier
        /// </summary>
        public long ChatUserId {
            get {
                return _message.Chat.Id;
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

        /// <summary>
        /// c-tor, Initializes an instance of Telegram.Bot.Types.Update
        /// </summary>
        /// <param name="update">Instance of Telegram.Bot.Types.Update</param>
        public Message(Telegram.Bot.Types.Update update) {
            _message = update.Message;
            Response = null;
        }
    }
}