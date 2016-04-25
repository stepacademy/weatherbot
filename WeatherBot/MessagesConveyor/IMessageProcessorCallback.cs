///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;

namespace WeatherBot.MessagesConveyor {

    using TeleInteraction.Adapters;

    [ServiceContract]
    public interface IMessageProcessorCallback {

        [OperationContract(IsOneWay = true)]
        void CallbackInvoke(Message message);

    }
}