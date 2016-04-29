///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///



namespace WeatherBot.MessagesConveyor.IO.ReplyProcessing {

    using TeleInteraction.Adapters;
    using DatabaseWorker.QueryComponents;

    internal interface IReplyProcessor {

        Message FormReply(QueryData response);


    }
}