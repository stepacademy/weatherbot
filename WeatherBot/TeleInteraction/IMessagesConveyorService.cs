using System.ServiceModel;

namespace WeatherBot.TeleInteraction
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof (IMessageProcessor))]
    public interface IMessagesConveyorService
    {
        [OperationContract(IsOneWay = true)]
        void Start();
    }
}