///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using Owin;
using System.Web.Http;
using System.Threading;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Message = WeatherBot.TeleInteraction.Adapters.Message;

namespace WeatherBot.TeleInteraction.InteractionStrategy {

    internal class BasedWebHook : IInteractionStrategy {                   // WebHook-based interaction mode, NEED TESTS

        private IMessageProcessorCallback _currentOperationContext;

        private Task Process() {

            using (WebApp.Start<WebHookStartup>("https://+:8443")) {

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
            _currentOperationContext = OperationContext.Current.GetCallbackChannel<IMessageProcessorCallback>();

            if (Thread.CurrentThread.ThreadState != ThreadState.WaitSleepJoin)
                await Process();
        }

        public void Stop() {
            if (Thread.CurrentThread.ThreadState == ThreadState.WaitSleepJoin)
                Thread.CurrentThread.Interrupt();
        }

        public void Receive(Telegram.Bot.Types.Update update) {
            _currentOperationContext.CallbackInvoke(new Message(update));
        }
    }

    internal class WebHookStartup {

        public void Configuration(IAppBuilder app) {

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute("WebHook", "{controller}");
            app.UseWebApi(configuration);
        }
    }
}