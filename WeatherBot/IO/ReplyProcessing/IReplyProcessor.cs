///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///



namespace WeatherBot.IO.ReplyProcessing {

    using DatabaseWorker.QueryComponents;
    using MessagesConveyor.TeleInteraction.Adapters;

    internal interface IReplyProcessor {

        Message FormReply(QueryData response);


    }
}