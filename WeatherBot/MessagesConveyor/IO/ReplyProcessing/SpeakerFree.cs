///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;

namespace WeatherBot.MessagesConveyor.IO.ReplyProcessing {

    using TeleInteraction.Adapters;
    using DatabaseWorker.QueryComponents;

    internal class SpeakerFree : IReplyProcessor {

        public Message FormReply(QueryData response) {
            throw new NotImplementedException();
        }
    }
}