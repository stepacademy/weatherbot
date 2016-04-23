///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;
using WeatherBot.TeleInteraction.Adapters;

namespace WeatherBot.TeleInteraction {

    [ServiceContract]
    public interface IMessageProcessorCallback {

        [OperationContract(IsOneWay = true)]
        void CallbackInvoke(Message message);
    }
}