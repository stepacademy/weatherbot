using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBot.MessagesConveyor.TeleInteraction;
using WeatherBot.MessagesConveyor.TeleInteraction.Adapters;

namespace WeatherBot.MessagesConveyor.Teleinteraction.InteractionStrategy {

    internal class Responder {

        public async void SendResponse(Message message) {

            if (message != null && message.Response != null) {

                if (message.Response.Text != null)
                    await Bot.Api.SendTextMessage(message.User.Id, message.Response.Text);
            }
        }
    }
}