///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///



namespace WeatherBot.IO.InputParse {

    using DatabaseWorker.QueryComponents;
    using MessagesConveyor.TeleInteraction.Adapters;

    internal interface IInputParser {

        QueryData FormQuery(Message message);


    }
}