///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;

namespace WeatherBot.IO.InputParse {

    using DatabaseWorker.QueryComponents;
    using MessagesConveyor.TeleInteraction.Adapters;

    internal class BasedMlInputParser : IInputParser {

        public QueryData FormQuery(Message message) {
            throw new NotImplementedException();
        }
    }
}