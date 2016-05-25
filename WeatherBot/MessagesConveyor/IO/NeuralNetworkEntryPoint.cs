///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

namespace WeatherBot.MessagesConveyor.IO {

    using TeleInteraction.Adapters;
    using TeleInteraction.InteractionStrategy;

    internal sealed class NeuralNetworkEntryPoint {

        private void Incoming(Message message) {
            return;                                                // <-- This line will be replaced to Machine Learning
        }

        public NeuralNetworkEntryPoint(IInteractionStrategy sender) {
            sender.Incoming += Incoming;
        }
    }
}