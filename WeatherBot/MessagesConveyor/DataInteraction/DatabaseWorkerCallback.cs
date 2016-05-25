﻿///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

namespace WeatherBot.MessagesConveyor.DataInteraction {

    using IO;
    using DatabaseQueryHandler;
    using DatabaseWorker.QueryComponents;

    internal class DatabaseWorkerCallback : IQueryHandlerContractCallback {

        private WeatherSpeaker  _speaker;

        public void Response(QueryData response) {
            _speaker.Response(response);
        }

        public DatabaseWorkerCallback() {
            _speaker = new WeatherSpeaker();
        }
    }
}