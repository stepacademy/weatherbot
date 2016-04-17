using WeatherBot.TeleInteraction.TelegramAdapters;


namespace WeatherBot.TeleInteraction {

    public interface ITeleInteractor {

        /// <summary>
        /// returns the next message from the queue if present, otherwise - returns null
        /// </summary>
        Message GetNextMessage();

        /// <summary>
        /// receives an instance of the Message class field filled with Response
        /// </summary>
        /// <param name="response">instance of the Message class</param>
        void SendResponse(Message response);

    }
}