///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.Runtime.Serialization;

namespace WeatherBot.MessagesConveyor.TeleInteraction.Adapters {
    
    [DataContract]
    public class Message {

        [DataMember] public readonly int       Id;
        [DataMember] public readonly DateTime  DTime;
        [DataMember] public readonly string    Text;
        [DataMember] public readonly MUser     User;
        [DataMember] public readonly MLocation Location;
        [DataMember] public MResponse          Response;

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