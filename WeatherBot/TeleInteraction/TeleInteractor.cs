using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.TeleInteraction {

    class TeleInteractor : ITeleInteractor {

        private string _botToken;

        private Telegram.Bot.Api _bot;
        private Telegram.Bot.Types.User _me;

        public async Task<Message> GetNextMessageAsync() {
            
            //dummy await

            return new Message();
        }
    }
}