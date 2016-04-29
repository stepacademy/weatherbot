using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy;

namespace WeatherBot.MessagesConveyor {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Management : IManagementContract {

        private IInteractionStrategy _interaction;
        public static string BotToken;

        private void SetTelegramInteractionMode(InteractionMode iMode) {

            if (iMode == InteractionMode.WebHookBased)
                _interaction = new BasedWebHook();
            else
                _interaction = new BasedGetUpdates();
        }

        public void Start(string botToken, InteractionMode iMode = InteractionMode.GetUpdatesBased) {
            BotToken = botToken;
            SetTelegramInteractionMode(iMode);
            _interaction.Start();
        }

        public void Stop() {
            _interaction.Stop();
        }


        public Management() {
            _interaction = new BasedGetUpdates();
        }        
    }
}