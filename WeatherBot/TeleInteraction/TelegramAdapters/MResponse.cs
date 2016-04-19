
/// MessageResponse class / Stanislav Kuzmich / Art.Stea1th

namespace WeatherBot.TeleInteraction.TelegramAdapters {

    public class MResponse {

        /// <summary>
        /// response Message Text (Optional)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// response Message Document - Database Id or path (Optional)
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// response Message Sticker - Telegram Sticker-Id (Optional)
        /// </summary>
        public string Sticker { get; set; }

    }
}