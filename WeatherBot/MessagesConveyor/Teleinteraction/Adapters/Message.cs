///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;

namespace WeatherBot.MessagesConveyor.TeleInteraction.Adapters {
    
    internal sealed class Message {

        public readonly int       Id;
        public readonly DateTime  DTime;
        public readonly string    Text;
        public readonly User      User;
        public readonly Location  Location;

        public Message(Telegram.Bot.Types.Update update) {

            Id       = update.Message.MessageId;
            DTime    = update.Message.Date;
            Text     = update.Message.Text;
            User     = new User(update.Message.From);
            Location = new Location(update.Message.Location);
        }
    }
}