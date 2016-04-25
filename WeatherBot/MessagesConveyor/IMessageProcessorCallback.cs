///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;
using WeatherBot.MessagesConveyor.TeleInteraction.Adapters;

namespace WeatherBot.MessagesConveyor {

    [ServiceContract]
    public interface IMessageProcessorCallback {

        [OperationContract(IsOneWay = true)]
        void CallbackInvoke(Message message);
    }
}