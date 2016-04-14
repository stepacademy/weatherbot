using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeatherBot.TeleInteraction {

    public interface ITeleInteractor {

        /// <summary>
        /// GetUpdatesAsync(); - A temporary dummy, will be return the built-in type or Generic
        /// </summary>

        // Task<Telegram.Bot.Types.Update[]> GetUpdatesAsync();

        // 1. use: Package Manager Console > "Restore" button
        // 2. uncomment line 16 for test correctly restore packages

        // otherwise:
        // 1. use Package Manager Console
        // 2. PM > Install-Package Telegram.Bot

    }
}