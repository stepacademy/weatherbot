namespace WeatherBot.TeleInteraction {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITeleInteractor {

        /// <summary>
        /// GetNextMessageAsync();
        /// </summary>
        Message GetNextMessage();

    }
}