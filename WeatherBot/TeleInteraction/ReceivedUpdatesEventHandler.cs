using System.Collections.Generic;
using Telegram.Bot.Types;

namespace WeatherBot.TeleInteraction
{
    public delegate void ReceivedUpdatesEventHandler(Queue<Update> updatesQueue);
}