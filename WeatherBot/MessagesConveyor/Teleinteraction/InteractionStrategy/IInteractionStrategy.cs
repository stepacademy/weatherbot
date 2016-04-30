///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {
    
    using IO;

    internal interface IInteractionStrategy {

        event MessageIncomingEvent Incoming;

        void Start();
        void Stop();
    }
}