namespace WeatherBot.TeleInteraction {

    using System.ServiceModel;

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IMessageProcessor))]
    public interface IReceiveActionListenerService {

        [OperationContract(IsOneWay = true)]
        void Start();

    }
}