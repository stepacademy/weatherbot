﻿///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor.IO {

    using DataInteraction;
    using TeleInteraction.Adapters;
    using TeleInteraction.InteractionStrategy;
    using DatabaseWorker.QueryComponents;

    internal sealed class LogicalInputParser {

        private DatabaseWorkerProxy _database;

        private void Incoming(Message message) {

            QueryData query = new QueryData();                           // <-- This code will be replaced to real parse
            query.InitiatorId = message.User.Id;
            query.City = "Минск";
            query.weatherAtTimes = new Dictionary<DateTime, WeatherEntities>();
            query.weatherAtTimes.Add(DateTime.Now, null);                // <-- This code will be replaced to real parse

            _database.Query(query);
        }

        public LogicalInputParser(IInteractionStrategy sender, DatabaseWorkerProxy proxy) {
            sender.Incoming += Incoming;
            _database = proxy;
        }
    }
}