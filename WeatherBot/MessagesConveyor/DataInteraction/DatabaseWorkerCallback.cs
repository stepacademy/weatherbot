///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

namespace WeatherBot.MessagesConveyor.DataInteraction {

    using DatabaseQueryHandler;
    using DatabaseWorker.QueryComponents;
    using TeleInteraction.InteractionStrategy;

    internal class DatabaseWorkerCallback : IQueryHandlerContractCallback {

        private OutcomingSender  _sender;


        public void Response(QueryData response) {
            _sender.Response(response);
        }

        public DatabaseWorkerCallback() {
            _sender = new OutcomingSender();
        }
    }
}