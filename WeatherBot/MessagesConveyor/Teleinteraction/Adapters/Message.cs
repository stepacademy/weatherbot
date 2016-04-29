///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;

namespace WeatherBot.MessagesConveyor.TeleInteraction.Adapters {
    
    internal class Message {

        public readonly int       Id;
        public readonly DateTime  DTime;
        public readonly string    Text;
        public readonly MUser     User;
        public readonly MLocation Location;
        public MResponse          Response;

        public Message(Telegram.Bot.Types.Update update) {

            Id       = update.Message.MessageId;
            DTime    = update.Message.Date;
            Text     = update.Message.Text;
            User     = new MUser(update.Message.From);
            Location = new MLocation(update.Message.Location);
            Response = null;
        }
    }
}