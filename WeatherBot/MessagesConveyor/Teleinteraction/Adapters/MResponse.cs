///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Runtime.Serialization;

namespace WeatherBot.MessagesConveyor.TeleInteraction.Adapters {

    [DataContract]
    public class MResponse {

        [DataMember] public string Text;
        [DataMember] public string Document;
        [DataMember] public string Sticker;
    }
}