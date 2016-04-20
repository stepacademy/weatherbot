using System.Collections.Generic;

namespace WeatherBot.TeleInteraction
{
    public delegate void ReceivedUpdatesEventHandler(Queue<Telegram.Bot.Types.Update> updatesQueue);
}