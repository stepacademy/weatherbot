/// Message class / Stanislav Kuzmich / Art.Stea1th

using System;
using Telegram.Bot.Types;

namespace WeatherBot.TeleInteraction.TelegramAdapters
{
    public class Message
    {
        private readonly Telegram.Bot.Types.Message _message;

        public Message(Update update)
        {
            _message = update.Message;
            User = new MUser(update.Message.From);
            Location = new MLocation(update.Message.Location);
            Response = null;
        }

        /// <summary>
        ///     Unique message identifier
        /// </summary>
        public int Id
        {
            get { return _message.MessageId; }
        }

        /// <summary>
        ///     Date the message was sent in Unix time
        /// </summary>
        public DateTime DTime
        {
            get { return _message.Date; }
        }

        /// <summary>
        ///     The actual UTF-8 text of the message
        /// </summary>
        public string Text
        {
            get { return _message.Text; }
        }

        /// <summary>
        ///     Message sender
        /// </summary>
        public MUser User { get; }

        /// <summary>
        ///     Location
        /// </summary>
        public MLocation Location { get; }

        /// <summary>
        ///     new MessageResponse attach here
        /// </summary>
        public MResponse Response { get; set; }
    }
}