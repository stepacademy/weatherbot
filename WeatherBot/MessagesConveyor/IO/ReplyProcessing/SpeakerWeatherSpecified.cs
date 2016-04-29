///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;

namespace WeatherBot.MessagesConveyor.IO.ReplyProcessing {

    using TeleInteraction.Adapters;
    using DatabaseWorker.QueryComponents;
    using DatabaseQueryHandler;
    using System.ServiceModel;

    internal sealed class SpeakerWeatherSpecified : IReplyProcessor, IQueryHandlerContractCallback {

        public void Response(QueryData response) {
            throw new NotImplementedException();
        }
    }
}