///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;

namespace WeatherBot.MessagesConveyor {

    using IO;
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
            _parser                  = new InputParserEntryPoint(_interaction, _proxy);
            _neuralNetworkEntryPoint = new NeuralNetworkEntryPoint(_interaction);
        }

        public void Start(string botToken, InteractionMode iMode) {

            BotToken = botToken;
            InteractionInitialize(iMode);
            _interaction.Start();
        }

        public void Stop() {
            _interaction.Stop();
        }
    }
}