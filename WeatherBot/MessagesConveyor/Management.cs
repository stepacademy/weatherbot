///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;
using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor {

    using IO.InputParse;
    using TeleInteraction.InteractionStrategy;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal sealed class Management : IManagementContract {

        private IInteractionStrategy _interaction;
        private List<IInputParser>   _inputParsers;

        public static string BotToken;

        private IInteractionStrategy TeleInteraction(InteractionMode iMode) {

            if (iMode == InteractionMode.WebHookBased)
                return new BasedWebHook();
            else
                return new BasedGetUpdates();
        }

        private void ParsersInitialize(IInteractionStrategy incomingInitiator) {

            _inputParsers = new List<IInputParser>(2);
            _inputParsers.Add(new BasedLogicInputParser(incomingInitiator));
            _inputParsers.Add(new BasedMlInputParser   (incomingInitiator));
        }

        public void Start(string botToken, InteractionMode iMode = InteractionMode.GetUpdatesBased) {

            BotToken = botToken;
            ParsersInitialize(_interaction = TeleInteraction(iMode));
            DatabaseWorkerInstance.Proxy.Open();
            _interaction.Start();
        }

        public void Stop() {
            _interaction.Stop();
        }
    }
}