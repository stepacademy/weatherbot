///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;

namespace WeatherBot.MessagesConveyor.TeleInteraction {

    internal class Bot {

        private static readonly Lazy<Telegram.Bot.Api> _api =
            new Lazy<Telegram.Bot.Api>(() => new Telegram.Bot.Api(Management.BotToken));

        public static Telegram.Bot.Api Api { get { return _api.Value; } }

        private Bot() { }
    }
}