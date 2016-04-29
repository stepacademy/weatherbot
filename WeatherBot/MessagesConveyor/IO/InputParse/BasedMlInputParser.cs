///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;

namespace WeatherBot.MessagesConveyor.IO.InputParse {

    using TeleInteraction.Adapters;
    using TeleInteraction.InteractionStrategy;

    internal class BasedMlInputParser : IInputParser {

        public void FormQuery(Message message) {
            return;
        }

        public BasedMlInputParser(IInteractionStrategy sender) {
            sender.Incoming += FormQuery;
        }
    }
}