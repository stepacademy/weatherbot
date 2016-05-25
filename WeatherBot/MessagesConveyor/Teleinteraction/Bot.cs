///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using Telegram.Bot;

namespace WeatherBot.MessagesConveyor.TeleInteraction {

    internal sealed class Bot {

        private static readonly Lazy<Api> _api = new Lazy<Api>(() => new Api(Management.BotToken));

        public static Api Api { get { return _api.Value; } }

        private Bot() { }
    }
}