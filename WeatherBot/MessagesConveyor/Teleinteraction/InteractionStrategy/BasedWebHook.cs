///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    using Message = Adapters.Message;

    internal sealed class BasedWebHook : IInteractionStrategy {            // WebHook-based interaction mode, NEED TESTS

        public event MessageIncomingEvent Incoming;

        private Task Process() {

            using (WebApp.Start<BasedWebHookStartup>("https://+:8443")) {

                try {                                                                                // Register WebHook
                    Bot.Api.SetWebhook("https://azure.stepacademy.weatherbot:8443/WeatherBotWebHook").Wait();
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException) {                            // Thread Interrupt & Unregister WebHook
                    Bot.Api.SetWebhook().Wait();
                }
            }
            return new Task(null);
        }

        public async void Start() {
            if (Thread.CurrentThread.ThreadState != ThreadState.WaitSleepJoin)
                await Process();
        }

        public void Stop() {
            if (Thread.CurrentThread.ThreadState == ThreadState.WaitSleepJoin)
                Thread.CurrentThread.Interrupt();
        }

        public void Receive(Telegram.Bot.Types.Update update) {
            Incoming.Invoke(new Message(update));
        }
    }    
}