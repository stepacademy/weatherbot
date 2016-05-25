///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using Owin;
using System.Web.Http;

namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    internal sealed class BasedWebHookStartup {

        public void Configuration(IAppBuilder app) {

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute("WebHook", "{controller}");
            app.UseWebApi(configuration);

        }
    }
}