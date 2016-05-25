///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;

namespace WeatherBot.MessagesConveyor {

    using Input;
    using DataInteraction;
    using DatabaseQueryHandler;
    using TeleInteraction.InteractionStrategy;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal sealed class Management : IManagementContract {

        private IInteractionStrategy          _interaction;
        private InputParserEntryPoint         _parser;
        private NeuralNetworkEntryPoint       _neuralNetworkEntryPoint;

        private IQueryHandlerContractCallback _callbackProxy;
        private DatabaseWorkerProxy           _proxy;        

        public static string BotToken;
        private string _owmToken;

        private IInteractionStrategy TeleInteraction(InteractionMode iMode) {

            if (iMode == InteractionMode.WebHookBased)
                return new BasedWebHook();
            else
                return new BasedGetUpdates();
        }

        private void InteractionInitialize(InteractionMode iMode) {

            _interaction             = TeleInteraction(iMode);
            _callbackProxy           = new DatabaseWorkerCallback();
            _proxy                   = new DatabaseWorkerProxy(_callbackProxy);
            _parser                  = new InputParserEntryPoint(_interaction, _proxy, _owmToken);
            _neuralNetworkEntryPoint = new NeuralNetworkEntryPoint(_interaction);
        }

        public void Start(string botToken, string owmToken, InteractionMode iMode) {

            BotToken = botToken;
            _owmToken = owmToken;
            InteractionInitialize(iMode);
            _interaction.Start();
        }

        public void Stop() {
            _interaction.Stop();
        }
    }
}