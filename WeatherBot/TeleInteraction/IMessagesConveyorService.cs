namespace WeatherBot.TeleInteraction {

    using System.ServiceModel;

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IMessageProcessor))]
    public interface IMessagesConveyorService {

        [OperationContract(IsOneWay = true)]
        void Start();

    }
}