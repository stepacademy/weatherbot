///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor.IO.InputParse {

    using TeleInteraction.Adapters;    
    using TeleInteraction.InteractionStrategy;
    using DatabaseWorker.QueryComponents;

    internal sealed class BasedLogicInputParser : IInputParser {

        private QueryData DummyParse() {    // <-- This code part will be removed

            QueryData result = new QueryData();

            result.InitiatorId = 0;
            result.City = "Минск";            
            result.weatherAtTimes = new Dictionary<DateTime, WeatherEntities>();
            result.weatherAtTimes.Add(DateTime.Now, null);

            return result;
        }

        public void FormQuery(Message message) {

            //
            // ... parsing

            //
            // ... query

            DatabaseWorkerInstance.Proxy.QueryAsync(DummyParse());
        }

        public BasedLogicInputParser(IInteractionStrategy sender) {
            sender.Incoming += FormQuery;
        }
    }
}