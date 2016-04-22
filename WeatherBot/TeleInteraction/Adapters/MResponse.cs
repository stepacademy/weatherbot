///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Runtime.Serialization;

namespace WeatherBot.TeleInteraction.Adapters {

    [DataContract]
    public class MResponse {

        [DataMember] public string Text     { get; set; }
        [DataMember] public string Document { get; set; }
        [DataMember] public string Sticker  { get; set; }
    }
}